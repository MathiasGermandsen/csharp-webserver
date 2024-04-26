let display = document.getElementById('display');
let currentNumber = '';
let previousNumber = '';
let operation = '';

function appendNumber(number) {
  if (number === '.' && currentNumber.includes('.')) return;
  currentNumber += number;
  display.value = currentNumber;
}

function clearAll() {
  currentNumber = '';
  previousNumber = '';
  operation = '';
  display.value = 0;
}

function deleteNumber() {
  currentNumber = currentNumber.slice(0, -1);
  display.value = currentNumber;
}

function calculate() {
  if (currentNumber === '' || operation === '') return;
  previousNumber = parseFloat(previousNumber);
  let current = parseFloat(currentNumber);

  if (operation === '+') {
    current = previousNumber + current;
  } else if (operation === '-') {
    current = previousNumber - current;
  } else if (operation === '*') {
    current = previousNumber * current;
  } else if (operation === '/') {
    if (current === 0) {
      alert('Division by zero is not allowed!');
      return;
    }
    current = previousNumber / current;
  }

  previousNumber = '';
  currentNumber = current.toString();
  display.value = currentNumber;
  operation = '';
}

function operate(op) {
  if (currentNumber === '') return;
  if (operation !== '') {
    calculate();
  }
  previousNumber = currentNumber;
  currentNumber = '';
  operation = op;
}
