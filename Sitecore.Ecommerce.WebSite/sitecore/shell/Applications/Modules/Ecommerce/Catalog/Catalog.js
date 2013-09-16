var Catalog = Class.create({
  initialize: function () {

  },

  getSearchQuery: function () {
    var sms = "";
    $$(".scDropDownList option").each(function (rb) {
      if (rb.selected) {
        sms = "&sm=" + rb.id;
      }
    });

    var tbs = "";
    $$(".scSearch").each(function (tb) {
      if (tb.value) {
        tbs += "&" + tb.id + "=" + encodeURIComponent(tb.value);
      }
    });

    var cls = "";
    $$(".scChecklistItems").each(function (cl) {
      var clf = "";

      $$("div [name=" + $(cl).readAttribute("name") + "] input:checked").each(function (cb, i) {
        var sep = i > 0 ? "|" : "";
        clf += sep + cb.value;
      });

      if (clf != "") {
        cls += "&" + $(cl).readAttribute("name") + "=" + clf;
      }
    });

    return "&action=search" + sms + tbs + cls;
  },

  gridRowDoubleClick: function (gridItem) {
    if (gridItem == null) {
      return;
    }
    var rowID = gridItem.GetMember("RowID").Value;
    scForm.postEvent(this, '', "catalog:gridrowdoubleclick(rowID=" + rowID + ")");
  },

  gridRowSelect: function (gridItem) {
    var rowIDs = new Array();
    var rows = Grid.GetSelectedItems();

    $A(rows).each(function (selectedItem) {
      var rowID = selectedItem.GetMember("RowID").Value;
      rowIDs.push(rowID);
    });

    scForm.postEvent(this, '', "catalog:gridrowselect(rowIDs=" + rowIDs.join('|') + ")");
  },

  refreshGrid: function () {
    Grid.scHandler.refresh();
  },

  showRibbon: function () {
    scUpdateRibbonProxy("Ribbon", "Ribbon", true);
  }
});

var ProductCatalog = Class.create(Catalog, {
  initialize: function (selectedProducts) {
    this.selectedProducts = $(selectedProducts);

    document.observe("dom:loaded", function () {
      $$(".scSearch").each(function (tb) {
        tb.observe('change', function (e) {
          UpdateContentField();
        });
      });

      $$(".scChecklistItems input[type='checkbox']").each(function (cl) {
        cl.observe('change', function (e) {
          UpdateContentField();
        });
      });

      $$(".scDropDownList").each(function (rb) {
        rb.observe('change', function (e) {
          UpdateContentField();
        });
      });
    });
  },

  selectedProductsDoubleClick: function (gridItem) {
    var rowID = gridItem.GetMember("RowID").Value;
    scForm.postEvent(this, '', "productcatalog:spgridrowdoubleclick(rowID=" + rowID + ")");
  },

  addButtonClicked: function () {
    var rowIDs = new Array();

    $A(Grid.GetSelectedItems()).each(function (gridItem) {
      var rowID = gridItem.GetMember("RowID").Value;
      rowIDs.push(rowID);
    });

    scForm.postEvent(this, '', "productcatalog:addbuttonclick(rowIDs=" + rowIDs.join('|') + ")");
  },

  moveToSelected: function (allowedIDs) {
    $A(Grid.GetSelectedItems()).each(function (gridItem) {

      var rowID = gridItem.GetMember("RowID").Value;

      if ($A(allowedIDs.split('|')).indexOf(rowID) >= 0) {
        var table = ProductsGrid.get_table();
        var newRow = table.addEmptyRow();

        for (i = 0; i < table.get_columns().length; i++) {
          newRow.setValue(i, gridItem.getMemberAt(i).Value);
        }
      }
    })
    UpdateContentField();
  },

  unSelectAllRows: function () {
    Grid.unSelectAll();
  },

  removeSelectedProduct: function () {
    ProductsGrid.deleteSelected();
    UpdateContentField();
  }
});

var scCatalog = new Catalog();

function UpdateContentField() {
  var form = Sitecore.App.getParentForm();
  form.setModified(true);
}

function scGetFrameValue(value, request) {
  var frame = scForm.browser.getFrameElement(window);
  if (frame == null || frame.style.display == "none") {
    return;
  }

  if (request.parameters == "contenteditor:save" || request.parameters == "item:save") {
    Sitecore.App.invoke("item:save");
  }

  return null;
}