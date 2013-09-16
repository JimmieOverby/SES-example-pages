$(document).ready(function () {

  $('.treeview').height($(window).height() - 74 + 'px');
  $('.stackTrace').height($(window).height() - 452 + 'px');

  $('.treeview .node').click(function (event) {
    $(this).siblings('ul').toggle();
    event.stopImmediatePropagation();

    $(this).toggleClass('collapsed');

    var collapsed = new Array();
    $('.node.collapsed').each(function () {
      collapsed.push($(this).next().next().text());
    });
    $.cookie('collapsed', collapsed.join('|'), { expires: 7 });

    event.stopImmediatePropagation();
  });

  $('.treeview .title').click(function (event) {
    $('.treeview .title').removeClass('selected');
    $(this).addClass('selected');

    $('#run').removeAttr('disabled');
  });

  $('#run').click(function () {
    tree.refresh();
    tree.runTests();
  });

  $('.treeview .title').dblclick(function () {
    tree.refresh();
    tree.runTests();
  });

  // Collapse nodes
  $($.cookie('collapsed').split('|')).each(function () {
    var title = this;
    $('.title').each(function () {
      if ($(this).text() == title) {
        $(this).prev().prev().click();
      }
    })
  });
});

function treeview() {
  return {
    refresh: function () {
      $('.treeview .state').attr('class', 'state notrun');
      $('#ProgressBar').attr('class', 'result');
      $('#MessageBox').html('');
      $('#StackTraceBox').html('');
    },

    runTests: function () {
      $('.treeview .title.selected').parent().find('.node.last').each(function () {
        var title = $(this).next().next();
        var testId = title.attr('id');

        // Fixes styles of the current node.
        setTimeout(function () {
          title.prev().attr('class', 'state running');

          $.ajax({
            async: false,
            type: "GET",
            url: "Default.aspx",
            data: "id=" + testId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
              var state = msg.d[0].toLowerCase();
              var prevstate = $('#testTreeRoot').prev().attr('class').split(' ')[1];

              var condition = '[class != "state failure"]';
              if (state == 'success') {
                condition += '[class != "state inconclusive"]';
              }

              $(title).parents('li').children('.state' + condition).attr('class', 'state ' + state);

              var progressClass = $('#testTreeRoot').prev().attr('class').split(' ')[1];
              $('#ProgressBar').attr('class', 'result ' + progressClass);

              $('#MessageBox').html($('#MessageBox').html() + msg.d[1]);
              $('#StackTraceBox').html($('#StackTraceBox').html() + msg.d[2]);
            },
            error: function (msg) {
              $(title).parents('li').children('.state').attr('class', 'state failure');
              $('#ProgressBar').attr('class', 'result failure');
            }
          });

          $('.messageBox ul li').click(function () {
            $('.messageBox ul li').removeClass('selected');
            $(this).addClass('selected');

            var id = this.id.replace('_mb', '_st');
            $('.stackTrace p').removeClass('selected');
            $('#' + id).addClass('selected');
          });
        }, 100);
      });
    }
  }
}

var tree = treeview();
