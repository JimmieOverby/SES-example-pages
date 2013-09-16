<%@ Page Language="C#" Debug="true" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Threading" %>
<script runat="server">
  protected override void OnLoad(EventArgs args)
  {
     Delegate cb = Thread.GetData(Thread.GetNamedDataSlot("WebTestRunner")) as Delegate;
     Thread.SetData(Thread.GetNamedDataSlot("WebTestRunner"), cb.DynamicInvoke(new object[0]));
  }
</script>