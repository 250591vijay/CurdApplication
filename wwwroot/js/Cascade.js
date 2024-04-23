$(document).ready(function ()
{
    /*  alert('OK');*/
    GetCountry();

    $('#State').attr('disabled', true);
    $('#City').attr('disabled', true);
    // to bind state
    $('#Country').change(function ()
    {
        $('#State').attr('disabled', false);
        var id = $(this).val();
        $('#State').empty();
        $('#State').append('<Option>--Select State--</Option>');
        $.ajax({
            //url: '/Cascade/State',
            url: '/Cascade/State?id=' + id,
           // data: { Id: d },
            success: function (result)
            {
                $.each(result, function (i, data)
                {
                    $('#State').append('<Option value=' + data.id + '>' + data.stateName + '</Option>');
                });              
            }

        });
    });
    // City
    $('#State').change(function ()
    {
        $('#City').attr('disabled', false);
        var id = $(this).val();
        $('#City').empty();
        $('#City').append('<Option>--Select City--</Option>');
        $.ajax({
            //url: '/Cascade/State',
            url: '/Cascade/City?id=' + id,
            // data: { Id: d },
            success: function (result)
            {
                $.each(result, function (i, data)
                {
                    $('#City').append('<Option value=' + data.id + '>' + data.cityName + '</Option>');
                });
            }

        });
    });
});

//callback function()
//Inside a function, pass such an argument to another function, then it is called callback function
// kisi function k ander dusre function ko call karte hai as an argument it is called callback
function GetCountry()
{
    $.ajax({
        url: '/Cascade/Country',
        // result is parameter jis m database s data aayega
        success: function (result) {
            // To callback function
            // result m jo v data ayega usko data m get karenge  
            // i index ho ga
            $.each(result, function (i,data){
                $('#Country').append('<Option value =' + data.id + '>' + data.countryName +'</Option>')
            });
        }
    });
}