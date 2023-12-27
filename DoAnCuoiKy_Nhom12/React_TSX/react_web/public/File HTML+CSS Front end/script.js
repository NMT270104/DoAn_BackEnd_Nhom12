function increment() {
  var input = document.getElementById("quantity");
  var value = parseInt(input.value, 10);
  value = isNaN(value) ? 0 : value;
  value++;
  input.value = value;
}

function decrement() {
  var input = document.getElementById("quantity");
  var value = parseInt(input.value, 10);
  value = isNaN(value) ? 0 : value;
  if (value > 1) {
    value--;
    input.value = value;
  }
}
