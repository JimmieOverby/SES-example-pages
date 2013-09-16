// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateControlAdapter.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved. 
// </copyright>
// <summary>
//   Allows to disable validation fot WFM controls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Web.UI.Adapters
{
  using System;
  using System.Web.UI;
  using System.Web.UI.Adapters;
  using System.Web.UI.WebControls;
  using Diagnostics;

  /// <summary>
  /// Allows to disable validation fot WFM controls.
  /// </summary>
  public class ValidateControlAdapter : ControlAdapter
  {
    /// <summary>
    /// Determines if control validators should be disabled.
    /// </summary>
    private readonly HiddenField hiddenField;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidateControlAdapter"/> class.
    /// </summary>
    public ValidateControlAdapter()
    {
      this.hiddenField = new HiddenField();
    }

    /// <summary>
    /// Gets a value indicating whether this control validation is disabled.
    /// </summary>
    /// <value><c>true</c> if disabled; otherwise, <c>false</c>.</value>
    private bool Disabled
    {
      get { return this.hiddenField.Value.Contains("disabled"); }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);

      bool first = false;
      for (int i = 0; i < Control.Controls.Count; i++)
      {
        if (Control.Controls[i] is Panel)
        {
          if (!first)
          {
            Control.Controls[i].Controls.Add(this.hiddenField);
            this.hiddenField.ID = this.Control.ID + string.Format("{0}_ecstate", i);
            first = true;
            continue;
          }

          var hidden = new HiddenField();
          hidden.ID = this.Control.ID + string.Format("{0}_ecstate", i);
          Control.Controls[i].Controls.Add(hidden);
        }
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      if (!this.Disabled)
      {
        return;
      }

      this.DisabledValidators(this.Control.Controls);
    }

    /// <summary>
    /// Disableds the validators.
    /// </summary>
    /// <param name="controls">The controls.</param>
    private void DisabledValidators(ControlCollection controls)
    {
      Assert.ArgumentNotNull(controls, "controls");

      foreach (Control control in controls)
      {
        BaseValidator validator = control as BaseValidator;
        if (validator != null)
        {
          validator.Enabled = false;
        }

        this.DisabledValidators(control.Controls);
      }
    }
  }
}