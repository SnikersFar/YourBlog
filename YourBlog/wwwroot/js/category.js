$(document).ready(function () {
    let MyId = $(".IdCategory");
    $(".edit-button").click(function () {
        let myId = $(this).attr('id');
        MyId.val(myId);
        $(".cancel-button").attr('type', 'button');
        $("legend").text("Change Category");
        let myName = $("#name-"+myId);
        $(".name-input").val(myName.text());
    });
    $(".cancel-button").click(function () {
        MyId.val(0);
        $(this).attr('type', 'hidden');
        $(".name-input").val("");
        $("legend").text("Create Category");
    });
});