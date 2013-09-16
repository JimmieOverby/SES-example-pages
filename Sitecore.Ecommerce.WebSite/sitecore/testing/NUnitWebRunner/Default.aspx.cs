// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The tests web runner page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.NUnit.WebRunner
{
  using System;
  using System.Collections.Generic;
  using System.Web;
  using System.Web.Services;
  using global::NUnit.Core;
  using global::NUnit.Core.Filters;
  using TreeNode = WebControls.TreeNode;

  /// <summary>
  /// The tests web runner page.
  /// </summary>
  public partial class Default : System.Web.UI.Page
  {
    /// <summary>
    /// The identifier of the test tree root node.
    /// </summary>
    private const string TestTreeRootId = "testTreeRoot";

    /// <summary>
    /// Gets or sets the test.
    /// </summary>
    /// <value>The test.</value>
    private static TestRunner TestRunner
    {
      get { return (TestRunner)HttpContext.Current.Session["TestRunner"]; }
      set { HttpContext.Current.Session["TestRunner"] = value; }
    }

    /// <summary>
    /// Gets or sets the test names.
    /// </summary>
    /// <value>The test names.</value>
    private static IDictionary<string, TestName> TestNames
    {
      get { return (IDictionary<string, TestName>)HttpContext.Current.Session["TestNames"]; }
      set { HttpContext.Current.Session["TestNames"] = value; }
    }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    /// <value>The category.</value>
    private static string Category
    {
      get { return (string)HttpContext.Current.Session["Category"]; }
      set { HttpContext.Current.Session["Category"] = value; }
    }

    /// <summary>
    /// Runs the test.
    /// </summary>
    /// <param name="id">The test id.</param>
    /// <returns>The test resuls.</returns>
    public string[] RunTest(string id)
    {
      string state = "failure";
      string message = string.Empty;
      string stackTrace = string.Empty;

      EventListener listener = new QueuingEventListener();

      try
      {
        ITestFilter filter = GetTestFilter(id);
        TestResult rootResult = TestRunner.Run(listener, filter);

        switch (rootResult.ResultState)
        {
          case ResultState.Success:
            {
              state = "success";
              break;
            }

          case ResultState.Ignored:
          case ResultState.Inconclusive:
          case ResultState.Skipped:
            {
              state = "inconclusive";
              break;
            }
        }

        TestResult testResult = GetInnerTestResult(rootResult, id);
        if (testResult != null && !testResult.IsSuccess)
        {
          string resId = HttpUtility.UrlEncode(testResult.FullName).Replace('.', '_').Replace('+', '_');
          message = string.Format("<li id=\"{0}_mb\"><dl><dt>{1}</dt><dd>{2}</dd></dl></li>", resId, testResult.FullName, testResult.Message);
          stackTrace = string.Format("<p id=\"{0}_st\">{1}</p>", resId, testResult.StackTrace);
        }
      }
      catch (Exception ex)
      {
        message = ex.Message;
        stackTrace = ex.StackTrace;
      }

      return new[] { state, message, stackTrace };
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
      string testId = Request["id"];
        
      if (!string.IsNullOrEmpty(testId))
      {
        IDictionary<string, string[]> result = new Dictionary<string, string[]> { { "d", this.RunTest(testId) } };
        this.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        this.Response.End();
        
        return;
      }

      string assembly = Request["assembly"] ?? Request["a"];
      if (string.IsNullOrEmpty(assembly))
      {
        assembly = "Sitecore.NUnit.WebRunner.dll";
      }

      Category = HttpContext.Current.Request["category"] ?? HttpContext.Current.Request["c"] ?? string.Empty;

      this.LoadTests(assembly);

      TreeNode root = new TreeNode(assembly)
      {
        Id = TestTreeRootId,
        CssClass = "notrun"
      };
      this.TreeView.Nodes.Add(root);

      this.BuildTree(root, TestRunner.Test);

      this.ProgressBar.CssClass = "result";
    }

    /// <summary>
    /// Gets the test filter.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The test filter.</returns>
    private static ITestFilter GetTestFilter(string id)
    {
      List<ITestFilter> innerFilters = new List<ITestFilter>();

      if (id != TestTreeRootId)
      {
        TestName nameFilter = TestNames[id];
        innerFilters.Add(new NameFilter(nameFilter));
      }

      if (!string.IsNullOrEmpty(Category))
      {
        CategoryFilter catecoryFilter = new CategoryFilter(Category);
        innerFilters.Add(catecoryFilter);
      }

      ITestFilter filter = new AndFilter(innerFilters.ToArray());
      return filter;
    }

    /// <summary>
    /// Loads the test results.
    /// </summary>
    /// <param name="baseResult">The base result.</param>
    /// <param name="id">The id.</param>
    /// <returns>The inner test result.</returns>
    private static TestResult GetInnerTestResult(TestResult baseResult, string id)
    {
      if (!baseResult.HasResults)
      {
        return null;
      }

      foreach (TestResult result in baseResult.Results)
      {
        if (result == null || string.IsNullOrEmpty(result.Name))
        {
          continue;
        }

        if (!result.IsSuccess && !result.HasResults && result.Test.TestName.TestID.ToString() == id)
        {
          return result;
        }

        return GetInnerTestResult(result, id);
      }

      return null;
    }

    /// <summary>
    /// Builds the tree.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="test">The test result.</param>
    private void BuildTree(TreeNode node, ITest test)
    {
      if (test.Tests == null)
      {
        return;
      }

      foreach (ITest innerTest in test.Tests)
      {
        if (innerTest == null || string.IsNullOrEmpty(innerTest.TestName.FullName))
        {
          continue;
        }

        bool notInCategory = false;
        if ((innerTest.Tests == null || innerTest.Tests.Count == 0) && !string.IsNullOrEmpty(Category))
        {
          notInCategory = !innerTest.Categories.Contains(Category);
        }

        TreeNode childNode = new TreeNode(innerTest.TestName.Name)
        {
          Id = innerTest.TestName.TestID.ToString(),
          CssClass = "notrun",
          NotInCategory = notInCategory
        };

        node.ChildNodes.Add(childNode);

        this.BuildTree(childNode, innerTest);
      }
    }

    /// <summary>
    /// Runs the tests.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <exception cref="InvalidOperationException">Test assembly required.</exception>
    private void LoadTests(string assembly)
    {
      string path = string.Concat(HttpContext.Current.Request.PhysicalApplicationPath, @"bin\", assembly);

      TestPackage package = new TestPackage(path);
      package.Settings["UseThreadedRunner"] = false;

      TestRunner runner = new RemoteTestRunner();
      runner.Load(package);

      TestRunner = runner;

      IDictionary<string, TestName> testNames = new Dictionary<string, TestName>();
      this.LoadTestNames(runner.Test, testNames);
      TestNames = testNames;
    }

    /// <summary>
    /// Gets the test name by id.
    /// </summary>
    /// <param name="test">The test.</param>
    /// <param name="testNames">The test names.</param>
    private void LoadTestNames(ITest test, IDictionary<string, TestName> testNames)
    {
      if (test == null)
      {
        return;
      }

      if (test.Tests == null || test.Tests.Count == 0)
      {
        TestName testName = test.TestName;
        testNames.Add(testName.TestID.ToString(), testName);
      }
      else
      {
        foreach (ITest subTest in test.Tests)
        {
          this.LoadTestNames(subTest, testNames);
        }
      }
    }
  }
}