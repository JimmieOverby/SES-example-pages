namespace Sitecore.Ecommerce.Classes.Validators.FieldValidators
{
  using System;
  using System.Globalization;
  using System.Runtime.Serialization;
  using Sitecore.Data;
  using Sitecore.Data.Validators;
  using Globalization;
  using IO;
  using Telerik.WebControls;

  /// <summary>
  /// </summary>
  [Serializable]
  public class TwoSpellingMisprints : StandardValidator
  {
    /// <summary>
    /// </summary>
    private const string FileUtilMapPath = "/sitecore/shell/RadControls/Spell/TDF/";

    /// <summary>
    /// Initializes a new instance of the <see cref="TwoSpellingMisprints"/> class.
    /// </summary>
    public TwoSpellingMisprints()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TwoSpellingMisprints"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    public TwoSpellingMisprints(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public override string Name
    {
      get
      {
        return "Spellcheck";
      }
    }

    /// <summary>
    /// Evaluate spelling
    /// </summary>
    /// <returns>
    /// The result of the evaluation.
    /// </returns>
    protected override ValidatorResult Evaluate()
    {
      string controlValidationValue = this.ControlValidationValue;
      if (string.IsNullOrEmpty(controlValidationValue))
      {
        return ValidatorResult.Valid;
      }

      ItemUri itemUri = this.ItemUri;
      if (itemUri == null)
      {
        return ValidatorResult.Valid;
      }

      CultureInfo cultureInfo = itemUri.Language.CultureInfo;
      if (cultureInfo.IsNeutralCulture)
      {
        cultureInfo = Language.CreateSpecificCulture(cultureInfo.Name);
      }

      var checker = new SpellChecker(FileUtil.MapPath(FileUtilMapPath))
      {
        Text = controlValidationValue, 
        DictionaryLanguage = cultureInfo.Name
      };
      if (checker.CheckText().Count <= 2)
      {
        return ValidatorResult.Valid;
      }

      this.Text = string.Format("The field \"{0}\" contains more than two spelling errors.", this.GetFieldDisplayName());

      return this.GetFailedResult(ValidatorResult.Error);
    }

    /// <summary>
    /// Gets the failed results
    /// </summary>
    /// <returns>
    /// The max validator result.
    /// </returns>
    /// <remarks>
    /// This is used when saving and the validator uses a thread. If the Max Validator Result
    /// is Error or below, the validator does not have to be evaluated before saving.
    /// If the Max Validator Result is CriticalError or FatalError, the validator must have
    /// been evaluated before saving.
    /// </remarks>
    protected override ValidatorResult GetMaxValidatorResult()
    {
      return this.GetFailedResult(ValidatorResult.Error);
    }
  }
}