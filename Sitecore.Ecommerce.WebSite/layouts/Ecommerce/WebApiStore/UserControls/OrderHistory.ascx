<%@ Control Language="C#" AutoEventWireup="true" %>
<script type="text/javascript">
  function getOrders() {
    $.getJSON("api/orderHistory",
            function (data) {
              $('#orders').empty();

              $.each(data, function (key, val) {
                var str = val.OrderNumber;
                
                $('<li/>', { text: str })
                .appendTo($('#orders'));
              });
            });
  }

  $(document).ready(getOrders);
</script>
<h2>
  Orders</h2>
<ul id="orders" />
