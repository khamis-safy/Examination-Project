    // Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
    // for details on configuring this project to bundle and minify static web assets.

    // Write your JavaScript code.
    function addOption() {
        const optionsContainer = document.getElementById('options-container');
    const optionCount = optionsContainer.querySelectorAll('.form-check').length;

    const optionDiv = document.createElement('div');
    optionDiv.classList.add('form-check', 'mb-2');
    optionDiv.innerHTML = `
    <input class="form-check-input" type="radio" name="answer" id="option${optionCount + 1}" value="">
        <label class="form-check-label w-75" for="option${optionCount + 1}">
            <input type="text" class="form-control" placeholder="Enter option" onchange="updateOptionLabel(this, ${optionCount+1})">
        </label>
        <button type="button" class="btn btn-outline-danger btn-sm ml-2" onclick="removeOption(this)">Remove</button>
        `;

        optionsContainer.appendChild(optionDiv);
    }

        function updateOptionLabel(input, optionIndex) {
        const label = input.parentElement;
        const radioInput = label.previousElementSibling;
        radioInput.value = input.value;
    }

        function removeOption(button) {
        const optionDiv = button.parentElement;
        const optionsContainer = optionDiv.parentElement;
        optionsContainer.removeChild(optionDiv);
    }
        // Get references to the select element and the div to show/hide
        const questionTypeSelect = document.getElementById('QuestionType');
        const optionsDiv = document.getElementById('ChoicesDiv');
        const trueFalseDiv = document.getElementById('TrueFalseDiv');
        const questions = [];

        // Add an event listener to the select element
        questionTypeSelect.addEventListener('change', function () {
        // Get the selected value
        const selectedValue = questionTypeSelect.value;

        // Show or hide the optionsDiv based on the selected value
        if (selectedValue === 'Multiple Choices') {
            optionsDiv.style.display = 'block'; // Show the div
        trueFalseDiv.style.display = 'none'; // Hide the div
        } else if (selectedValue === 'True/False') {
            trueFalseDiv.style.display = 'block'; // Show the div
        optionsDiv.style.display = 'none'; // Hide the div
        } else {
            trueFalseDiv.style.display = 'none'; // Hide the div
        optionsDiv.style.display = 'none'; // Hide the div

        }
    });
        function saveQuestion() {
        const questionText = document.getElementById('QuestionText');
        const questionType = document.getElementById('QuestionType');
        const questionMarks = document.getElementById('QuestionMarks'); // Get the "Question Marks Count" input
        const optionsContainer = document.getElementById('options-container');
        const choices = [];

        // Get all the choices from the optionsContainer
        optionsContainer.querySelectorAll('.form-check-input').forEach(function (input) {
            choices.push({
                text: input.nextElementSibling.querySelector('input[type="text"]').value,
                selected: input.checked, // Check if the radio button is selected
            });
        });

        if (questionText.value.trim() === '') {
            alert('Please enter a question text.');
        return;
        }
        if (questionMarks.value.toString().trim() === '') {
            alert('Please enter a question marks.');
        return;
        }

        const table = document.querySelector('table tbody');
        const row = table.insertRow(-1);
        row.classList.add('table-secondary');

        const cell1 = row.insertCell(0);
        cell1.textContent = questionText.value;

        const cell2 = row.insertCell(1);
        cell2.textContent = questionType.value;

        const cell3 = row.insertCell(2);
        cell3.textContent = questionMarks.value; // Display "Question Marks Count"

        const cell4 = row.insertCell(3); // Add a cell for actions
        const deleteButton = document.createElement('button');
        deleteButton.className = 'btn btn-outline-danger btn-sm';
        deleteButton.textContent = 'Delete';
        deleteButton.onclick = function () {
            // Get the row index by searching for the parent row element
            const rowIndex = Array.from(table.rows).indexOf(row);
        if (rowIndex !== -1) {
            table.deleteRow(rowIndex);
            }
        };
        cell4.appendChild(deleteButton);

        const question = {
            text: questionText.value,
        type: questionType.value,
        choices: choices,
        marks: questionMarks.value, // Store "Question Marks Count"
        };
        questions.push(question);

        // Clear input fields after adding the question
        questionText.value = '';
        questionType.value = 'Essay';
        questionMarks.value = ''; // Clear "Question Marks Count" input
        optionsDiv.style.display = 'none'; // Reset the display for the optionsDiv
        trueFalseDiv.style.display = 'none'; // Reset the display for the trueFalseDiv
    }   // Initialize the visibility based on the initial select value
        questionTypeSelect.dispatchEvent(new Event('change'));


        // Add an event listener to the table rows
        function resetQuestionDetails() {
            document.getElementById('detailQuestionText').textContent = '';
        document.getElementById('detailQuestionType').textContent = '';
        const choicesList = document.getElementById('detailQuestionChoices');
        choicesList.innerHTML = '';
    }

        // Add an event listener to the table rows
        const table = document.querySelector('table tbody');
        table.addEventListener('click', function (event) {
        if (event.target.tagName === 'TD') {
            // Get the parent row of the clicked cell
            const row = event.target.parentElement;

        // Get the row index
        const rowIndex = Array.from(table.rows).indexOf(row);

            // Check if the rowIndex is valid
            if (rowIndex >= 0 && rowIndex < questions.length) {
            // Reset old question details
            resetQuestionDetails();

        // Get the question object from the array
        const question = questions[rowIndex];

        // Set the details in the container
        document.getElementById('detailQuestionText').textContent = question.text;
        document.getElementById('detailQuestionType').textContent = question.type;
        document.getElementById('detailQuestionMarks').textContent = question.marks; // Set "Question Marks Count"

        const choicesList = document.getElementById('detailQuestionChoices');
        question.choices.forEach(function (choice) {
                    const listItem = document.createElement('li');
        listItem.textContent = choice.text;
        choicesList.appendChild(listItem);
                });

        // Show the container
        document.getElementById('questionDetails').style.display = 'block';
            }
        }
    });


        function submitQuestions() {
        const description = document.getElementById('Description').value;

        if (!description.trim()) {
            alert('Please enter a description.');
        return;
        }

        if (questions.length === 0) {
            alert('Please add at least one question.');
        return;
        }
            const ExamQuestions = questions.map(question => ({
            QuestionMarks: question.marks,
        QuestionText: question.text,
        QuestionType: question.type,
                QuestionChoices: question.choices.map(choice => ({
            IsCorrect: choice.selected,
        ChoiceText: choice.text,
                })),
            }));
        console.log(description)
        console.log(ExamQuestions)
        const requestData = {
            Description: description,
        ExamQuestions: ExamQuestions,
        };

        // Send an AJAX POST request to your backend endpoint
        fetch('/Exam/CreateExam', {
            method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            },
        body: JSON.stringify(requestData),
        })
            .then(data => {
            // Handle the response from the server as needed
                window.location.href = '/Exam/Index'; // Redirect to another URL on success

            })
            .catch(error => {
            console.error('Error:', error);
            });

    }

