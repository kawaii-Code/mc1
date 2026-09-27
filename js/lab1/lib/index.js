"use strict";

document.addEventListener('DOMContentLoaded', setup);
function setup() {
  document.getElementById('demoButton').onclick = addSomething;
}
function addSomething() {
  var someDummyDiv = document.createElement('div');
  someDummyDiv.classList.add('generated');
  var count = document.getElementsByClassName('generated').length;
  someDummyDiv.innerHTML = "I was created by JS! There are already ".concat(count, " of my friends!");
  var container = document.getElementById('container');
  container.appendChild(someDummyDiv);
}