function SetUniqueRadioButton(nameregex, current) {
  re = new RegExp(nameregex);
  for (i = 0; i < document.forms[0].elements.length; i++) {
    elm = document.forms[0].elements[i]
    if (elm.type == "radio") {
      if (re.test(elm.name)) {
        elm.checked = false;
      }
    }
  }
  current.checked = true;
}