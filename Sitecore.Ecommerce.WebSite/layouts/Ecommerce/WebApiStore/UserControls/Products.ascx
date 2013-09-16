<%@ Control Language="C#" AutoEventWireup="true" %>
<script type="text/javascript">
  function getProducts() {
    $.getJSON("api/products",
            function (data) {
              $('#products').empty(); // Clear the table body.

              // Loop through the list of products.
              $.each(data, function (key, val) {
                // Format the text to display.
                var str = val.Name + ': $' + val.Price;

                // Add a list item for the product.
                $('<li/>', { text: str })
                .appendTo($('#products'));
              });
            });
  }

  $(document).ready(getProducts);
</script>
<h2>
  Products</h2>
<ul id="products" />
