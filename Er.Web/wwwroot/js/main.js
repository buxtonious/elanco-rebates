document.addEventListener("DOMContentLoaded", function () {
    let validator = new Validator();
    let navigationManager = new NavigationManager(validator);

    registerNavigationButtons();
    registerSubmissionButton();
    registerFields();

    function registerNavigationButtons() {
        let nextButtons = document.querySelectorAll(".form-navigation .next");
        let backButtons = document.querySelectorAll(".form-navigation .back");

        for (let i = 0; i < nextButtons.length; i++) {
            nextButtons[i].addEventListener("click", function (event) {
                let currentStep = event.target.closest(".step").dataset.step;
                let stepToNavigateTo = event.target.dataset.next;

                navigationManager.navigateForward(currentStep, stepToNavigateTo);
            });
        }

        for (let i = 0; i < backButtons.length; i++) {
            backButtons[i].addEventListener("click", function (event) {
                let currentStep = event.target.closest(".step").dataset.step;
                let stepToNavigateTo = event.target.dataset.back;

                navigationManager.navigateBackward(currentStep, stepToNavigateTo);
            });
        }
    }

    async function registerSubmissionButton() {
        let form = document.querySelector("form");

        form.addEventListener("submit", async function (event) {
            event.preventDefault();

            let currentStep = event.submitter.closest(".step").dataset.step;
            await navigationManager.submitForm(currentStep);
        });
    }

    function registerFields() {
        let inputs = document.querySelectorAll('input');
        let selects = document.querySelectorAll('select');
        let fields = [...inputs, ...selects];

        for (let i = 0; i < fields.length; i++) {
            fields[i].addEventListener("change", function (event) {
                validator.clearValidationError(event.target);
            });
        }
    }
});

class NavigationManager {
    validator;

    constructor(validator) {
        this.validator = validator;
    }

    async submitForm(currentStep) {
        this.validator = new Validator(currentStep);
        let isValid = this.validator.validate();

        if (!isValid) {
            return;
        }

        let form = document.querySelector("form");
        let formData = new FormData(form);

        try {
            let response = await fetch("/Rebate", {
                method: "POST",
                body: formData
            });

            console.log(response);
        } catch (error) {
            console.log(error);
        }
    }

    navigateForward(currentStep, stepToNavigateTo) {
        this.validator = new Validator(currentStep);
        let isValid = this.validator.validate();

        if (!isValid) {
            return;
        }

        let currentStepContainer = document.querySelector(`.step[data-step="${currentStep}"]`);
        let nextStepContainer = document.querySelector(`.step[data-step="${stepToNavigateTo}"]`);

        currentStepContainer.style.display = "none";
        nextStepContainer.style.display = "block";
    }

    navigateBackward(currentStep, stepToNavigateTo) {
        let currentStepContainer = document.querySelector(`.step[data-step="${currentStep}"]`);
        let previousStepContainer = document.querySelector(`.step[data-step="${stepToNavigateTo}"]`);

        currentStepContainer.style.display = "none";
        previousStepContainer.style.display = "block";
    }
}

class Validator {
    currentStep;
    validationErrors;

    constructor(currentStep) {
        this.currentStep = currentStep;
        this.validationErrors = [];
    }

    validate() {
        this.clearValidationErrors();

        // find all required inputs and selects then merge into one array so we only need to create one loop
        let requiredInputs = document.querySelectorAll(`.step[data-step="${this.currentStep}"] input[required]`);
        let requiredSelects = document.querySelectorAll(`.step[data-step="${this.currentStep}"] select[required]`);
        let requiredFields = [...requiredInputs, ...requiredSelects];

        for (let i = 0; i < requiredFields.length; i++) {
            // checks the validity of the field using form validation
            let isFieldValid = requiredFields[i].checkValidity();

            if (isFieldValid) {
                continue;
            }

            // create the error message using the label name (e.g. first name, email)
            let labelText = requiredFields[i].labels[0].textContent;
            let errorMessage = this.createErrorMessage(requiredFields[i], labelText);

            // add the message to the validation errors, we also add a key which is the input ID
            // so we have an easier time removing errors from the array and validation summary we create later
            this.validationErrors.push({ key: requiredFields[i].id, message: errorMessage });
            this.createValidationElement(requiredFields[i], errorMessage);
        }

        this.createValidationSummary();

        if (!this.validationErrors || this.validationErrors.length > 0) {
            return false;
        }

        return true;
    }

    createErrorMessage(requiredField, labelText) {
        labelText = labelText.toLowerCase();
        let errorMessage = `Please enter your ${labelText}`;

        // uses form validation to determine what validation issue has occured
        // if none match we use the default message above
        if (requiredField.type == "select-one") {
            errorMessage = `Please select your ${labelText}`;
        } else if (requiredField.type == "radio") {
            errorMessage = `Please select a ${labelText}`;
        } else if (requiredField.type == "checkbox") {
            errorMessage = `Please tick ${labelText}`;
        }

        if (requiredField.validity.typeMismatch) {
            errorMessage = `Please enter ${labelText} in the correct format`;
        }

        return errorMessage;
    }

    createValidationElement(requiredField, errorMessage) {
        // creates a span element below the corresponding
        let inputContainer = requiredField.closest("div");
        let errorSpan = document.createElement("span");

        errorSpan.classList.add("validation-error");
        errorSpan.textContent = errorMessage;

        requiredField.classList.add("validation-error");
        inputContainer.appendChild(errorSpan);
    }

    createValidationSummary() {
        if (!this.validationErrors || this.validationErrors.length == 0) {
            return;
        }

        let validationSummaryElement = document.querySelector(".validation-summary");
        let alertElement = document.createElement("div");
        let alertHeading = document.createElement("h5");
        let alertUnorderedList = document.createElement("ul");

        alertElement.classList.add("alert", "alert-danger");
        alertElement.setAttribute("role", "alert");

        alertHeading.classList.add("alert-heading", "fw-bold");
        alertHeading.textContent = "Please enter something in these field(s):";

        for (let i = 0; i < this.validationErrors.length; i++) {
            let alertListItem = document.createElement("li");

            alertListItem.classList.add(this.validationErrors[i].key);
            alertListItem.textContent = this.validationErrors[i].message;

            alertUnorderedList.appendChild(alertListItem);
        }

        alertElement.appendChild(alertHeading);
        alertElement.appendChild(alertUnorderedList);

        validationSummaryElement.appendChild(alertElement);

        window.scrollTo(0, 0);
    }

    clearValidationError(field) {
        let key = field.id;

        for (let i = 0; i < this.validationErrors.length; i++) {
            if (this.validationErrors[i].key == key) {
                this.validationErrors.splice(i, 1);
            }
        }

        let alert = document.querySelector(".validation-summary .alert");
        let listItem = document.querySelector(`.validation-summary li.${key}`);
        let errorSpan = field.closest("div").querySelector("span.validation-error");

        if (listItem && errorSpan) {
            listItem.remove();
            errorSpan.remove();

            field.classList.remove("validation-error");
        }

        if (this.validationErrors.length == 0 && alert) {
            alert.remove();
        }
    }

    clearValidationErrors() {
        this.validationErrors = [];

        let spanElements = document.querySelectorAll("span.validation-error");
        let inputElements = document.querySelectorAll("input.validation-error");
        let selectElements = document.querySelectorAll("select.validation-error");
        let alertElement = document.querySelector('.validation-summary .alert');

        if (spanElements && spanElements.length > 0) {
            for (let i = 0; i < spanElements.length; i++) {
                spanElements[i].remove();
            }
        }

        if (inputElements && inputElements.length > 0) {
            for (let i = 0; i < inputElements.length; i++) {
                inputElements[i].classList.remove("validation-error");
            }
        }

        if (selectElements && selectElements.length > 0) {
            for (let i = 0; i < selectElements.length; i++) {
                selectElements[i].classList.remove("validation-error");
            }
        }

        if (alertElement) {
            alertElement.remove();
        }
    }
}