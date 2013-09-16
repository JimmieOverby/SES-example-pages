<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SampleControlTest.ascx.cs" Inherits="Sitecore.Ecommerce.Presentation.Tests.TestingSuite.Tests.SampleControlTest" %>

<script src="/layouts/Ecommerce/Sitecore.Ecommerce.Presentation.Tests/TestingSuite/SampleSystemUnderTest/sc.sample.widget.js"></script>
<input runat="server" type="button" id="SaveTheWorldButton" />

<script>
  module("sampleWidget", {
    setup: function () {
      var saveTheWorldButtonId = "<%= this.SaveTheWorldButton.ClientID %>";
      $saveTheWorldButton = $("#" + saveTheWorldButtonId).sampleWidget(
        { wouldYouLikeToSaveTheWorld: "Would you like to save the world?"
        });
    },
    teardown: function () {
    }
  });
  test("Should confirm saving of the world", function () {
    // arrange
    result = "";
    window.confirm = function (message) {
      result = message;
      return false;
    };
    // act
    $saveTheWorldButton.click();
    // assert
    equal(result, "Would you like to save the world?", "We expect the confirm message be 'Would you like to save the world?'");
  });
</script>
