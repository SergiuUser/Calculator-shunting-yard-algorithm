function addToExpression(value) {
    var expressionField = document.getElementById('expression');
    var expression = expressionField.value;

    var lastCharWasOperator = expression.length > 0 && isOperator(expression[expression.length - 1]);


    if (isOperator(value) && lastCharWasOperator)
        return

    if (value === '.') {
        var lastNumber = expression.split(/[-+*/^]/).pop();
        if (lastNumber.includes('.'))
            return;
        }

    expressionField.value += value;

}

function isOperator(char) {
    return ['+', '-', '*', '/', '.', '^'].includes(char);
}

function clearExpression() {
    document.getElementById('expression').value = "";
    document.getElementById('errorMessage').textContent = "";
}

function removeLastCharacter() {
    document.getElementById('expression').value = expression.value.slice(0, -1);
}

function evaluateExpression() {
    var expression = document.getElementById('expression').value;

    $.ajax({
        url: '/Home/Evaluate',
        cache: false,
        type: 'POST',
        data: JSON.stringify(expression),
        contentType: "application/json; charset=uft-8",
        success: function (response) {
            if (response.statusCode === 200) {
                clearExpression();
                document.getElementById('expression').value = response.expression;
            } else { }
                clearExpression();
                document.getElementById('errorMessage').textContent = response.expression;
            }
        },
    });
}