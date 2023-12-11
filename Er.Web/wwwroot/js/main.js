document.addEventListener("DOMContentLoaded", function () {
    const navigationManager = new NavigationManager();

    registerNavigationButtons();
    registerSubmissionButton();

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
            await navigationManager.submitForm(currentStep);
        });
    }
});

class NavigationManager {
    async submitForm() {
        var form = $("form");
        form.validate();

        if (!form.valid()) {
            return;
        }

        let response;

        try {
            response = await fetch("/Rebate/Submit", {
                method: "POST",
                body: form.serialize()
            });
        } catch (error) {
            console.log(error);
        }

        if (!response.ok) {
            let validationErrors = await response.json();

            this.validator.displayErrors(validationErrors);
        }
    }

    navigateForward(currentStep, stepToNavigateTo) {
        var form = $("form");
        form.validate();

        if (!form.valid()) {
            return;
        }

        let currentStepContainer = document.querySelector(`.step[data-step="${currentStep}"]`);
        let nextStepContainer = document.querySelector(`.step[data-step="${stepToNavigateTo}"]`);

        if (!nextStepContainer) {
            console.log("Failed to navigate forward, you should try again");

            return;
        }

        currentStepContainer.style.display = "none";
        nextStepContainer.style.display = "block";
    }

    navigateBackward(currentStep, stepToNavigateTo) {
        let currentStepContainer = document.querySelector(`.step[data-step="${currentStep}"]`);
        let previousStepContainer = document.querySelector(`.step[data-step="${stepToNavigateTo}"]`);

        if (!previousStepContainer) {
            console.log("Failed to navigate backward, you should try again");

            return;
        }

        currentStepContainer.style.display = "none";
        previousStepContainer.style.display = "block";
    }
}