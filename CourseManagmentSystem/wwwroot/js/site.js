// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



    $.ajax({alert('Hello!'); });
    $.ajax({
        url: '/Account/GetProfilePic',
    dataType: 'json',
    success: function (data) {
        alert('Hello!');
    var imgdata = data;
    var img = '<img src="data:image;base64,' + imgdata + '" alt="Profile Picture"  />';
    $("#your-image-container").html(img);
            }
        });