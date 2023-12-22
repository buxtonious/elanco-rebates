document.addEventListener("DOMContentLoaded", function () {
    registerNavigationButtons();
    registerUploadTriggers();
    registerSubmissionButton();

    function registerNavigationButtons() {
        let nextButtons = document.querySelectorAll(".form-navigation .next");
        let backButtons = document.querySelectorAll(".form-navigation .back");

        for (let i = 0; i < nextButtons.length; i++) {
            nextButtons[i].addEventListener("click", function (event) {
                let currentStep = event.target.closest(".step").dataset.step;
                let stepToNavigateTo = event.target.dataset.next;

                NavigationManager.navigateForward(currentStep, stepToNavigateTo);
            });
        }

        for (let i = 0; i < backButtons.length; i++) {
            backButtons[i].addEventListener("click", function (event) {
                let currentStep = event.target.closest(".step").dataset.step;
                let stepToNavigateTo = event.target.dataset.back;

                NavigationManager.navigateBackward(currentStep, stepToNavigateTo);
            });
        }
    }

    function registerUploadTriggers() {
        let uploadTriggers = document.querySelectorAll(".form-file-upload");

        for (let i = 0; i < uploadTriggers.length; i++) {
            uploadTriggers[i].addEventListener("change", function (event) {
                let file = event.target.files[0];

                FormManager.uploadFile(file);
            });
        }
    }

    function registerSubmissionButton() {
        let form = document.querySelector("form");

        form.addEventListener("submit", function (event) {
            event.preventDefault();
            FormManager.submitForm();
        });
    }
});

FormManager = {
    uploadFile: function (file) {
        let form = document.querySelector("form");
        let formData = new FormData(form);

        formData.append(file.name, file);

        let response = HttpClient.post("/Rebate/Upload", formData);

        if (!response.isSuccess) {
            console.log(response.message);
        }
    },

    submitForm: function () {
        var $form = $("form");

        if (!$form.valid()) {
            $form.validate();

            return;
        }

        let form = document.querySelector("form");
        let formData = new FormData(form);

        let response = HttpClient.post("/Rebate/Submit", formData);

        if (!response.isSuccess) {
            console.log(response.message);
        }

        $("#form-wrapper").html(response.data);
    }
}

NavigationManager = {
    navigateForward: function (currentStep, stepToNavigateTo) {
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
    },

    navigateBackward: function (currentStep, stepToNavigateTo) {
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

HttpClient = {
    post: function (url, data) {
        let response = {
            isSuccess: false,
            data: null,
            message: null
        };

        try {
            $.ajax({
                url: url,
                type: "POST",
                data: data,
                async: false,
                processData: false,
                contentType: false,
                success: function (result) {
                    response.isSuccess = true;
                    response.data = result;
                },
                error: function (result) {
                    console.log("An error occurred in the server: " + result);

                    response.isSuccess = false;
                    response.message = result.message;
                }
            });
        } catch (error) {
            console.log("An unexpected error occurred processing data: " + error);

            response.isSuccess = false;
            response.message = error.message;
        }

        return response;
    }
}