$(function () {
    $(document).on("keyup", "input[data-validate]", function () {
        validate();
    })
    $(document).on("click", "#saveEmploye", function () {
        if (validate()) {
            $("#editForm").submit();
        }
    })
    $(document).on("click", "#asc", function () {
        $("#ascDesc").val('asc');
    })
    $(document).on("click", "#desc", function () {
        $("#ascDesc").val('desc');
    })



})

function validate() {
    var validPhone = /^(0[1-9]{2})([0-9]{6})$/;
    var validEmail = /^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$/;
    var validName = /^[a-zA-z]+$/;
    var validAge = /^[1-9]?[0-9]{1}$|^100$/;

    let isError = true;
    $("input[data-validate]").each(function () {
        $this = $(this);
        let attributes = $this.attr("data-validate").split(",");
        $.each(attributes, function (index, item) {
            $this.next(".validateMessage").html("")
            if (item == "required") {
                if ($this.val() == "") {
                    $this.next(".validateMessage").html($this.attr("data-validate-name") + " Is Required")
                    isError = false;
                    return false;
                }
            }
            if (item == "email") {
                if (!$this.val().match(validEmail)) {
                    $this.next(".validateMessage").html($this.attr("data-validate-name") + " Is Invalid")
                    isError = false;
                }
            }
            if (item == "name") {
                if (!$this.val().match(validName)) {
                    $this.next(".validateMessage").html($this.attr("data-validate-name") + " Is Invalid")

                    isError = false;
                }
            }
            if (item == "age") {
                if (!$this.val().match(validAge)) {
                    $this.next(".validateMessage").html($this.attr("data-validate-name") + " Is Invalid")
                    isError = false;
                }
            }
            if (item == "phone") {
                if (!$this.val().match(validPhone)) {
                    $this.next(".validateMessage").html($this.attr("data-validate-name") + " Is Invalid")
                    isError = false;
                }
            }
        })
    })
    return isError;
}