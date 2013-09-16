(function ($) {
  $.widget('sc.sampleWidget', {
    options: {
      wouldYouLikeToSaveTheWorld: ''
    },

    _create: function () {
      options = this.options;

      this.addEvents();
    },

    addEvents: function () {
      var self = this;

      self.element.on('click', function () {
        if (confirm(self.options.wouldYouLikeToSaveTheWorld)) {
          // you can do something good here
        }
      });
    },

    destroy: function () {
      return $.Widget.prototype.destroy.call(this);
    }
  });
})(jQuery);