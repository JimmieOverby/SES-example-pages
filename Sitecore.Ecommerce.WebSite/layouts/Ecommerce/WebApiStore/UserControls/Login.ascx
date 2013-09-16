<%@ Control Language="C#" AutoEventWireup="true" %>
<script type="text/javascript">
  $(function () {
    $("#loginButton").live("click", function () {
      var login = $('#login').val();
      var pass = $('#password').val();

      $.getJSON("api/login?login=" + login + "&password=" + pass,
            function (data) {
              var str = data.Success;
              alert(str);
            });
      return false;
    });
  });
</script>
<div id="loginForm">
  <label for="login">
    Login:</label>
  <input id="login" type="text" />
  <label for="password">
    Password:</label>
  <input id="password" type="password" />
  <input id="loginButton" type="button" value="Login" />
</div>
