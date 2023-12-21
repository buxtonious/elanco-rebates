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

    function registerSubmissionButton() {
        let form = document.querySelector("form");

        form.addEventListener("submit", function (event) {
            event.preventDefault();
            navigationManager.submitForm();
        });
    }
});

class NavigationManager {

    submitForm() {
        var $form = $("form");

        if (!$form.valid()) {
            $form.validate();

            return;
        }

        $.ajax({
            url: "/Rebate/Submit",
            type: "POST",
            data: $form.serialize(),
            beforeSend: function () {

            },
            success: function (result) {
                if (!result.success) {
                    $("#form-wrapper").html(result);

                    // TODO: re-register button events
                }
            },
            error: function (result) {
                console.log(result);
            }
        });
    }

    navigateForward(currentStep, stepToNavigateTo) {
        var $form = $("form");

        if (!$form.valid()) {
            $form.validate();

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