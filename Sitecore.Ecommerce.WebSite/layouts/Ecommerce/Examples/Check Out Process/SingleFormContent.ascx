<%@ Control Language="c#" Debug="true" AutoEventWireup="true" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="page_body" class="clearfix">
    
    <%----------------------------------------
        --               content
        ----------------------------------------%>
    <div class="test">                
        <table id="formsTable" class="formsTable" runat="server">
         <tr>
            <td colspan="2">               
                  <sc:Placeholder runat="server" Key="phWarning" ID="Placeholder1"></sc:Placeholder>               
            </td>
         </tr>
         <tr>
            <td class="singleColumnForm">
               <div class="boxShaddow1">
                  <dl>
                     <dd>
                        <sc:Placeholder runat="server" Key="phLeftForm" ID="phLeftForm"></sc:Placeholder>
                     </dd>
                     <dd class="bottom">
                        <div>
                        </div>
                     </dd>
                  </dl>
               </div>
            </td>
         </tr>
      </table>        
    </div>
    <%----------------------------------------
        --               right side content
        ----------------------------------------%>
    <div id="pb_menu_secondary" class="pb_rc">        
        <sc:Placeholder runat="server" Key="phright" ID="phRight"></sc:Placeholder>
    </div>
    <div class="clear">
    </div>
</div>

