// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

jQuery(document).ready(function() {

    $('#select-showing').change(function (){
        $.ajax({
            url: "Products/Get",
            data: { 
                pageSize: $('#select-showing option:selected').val(), 
                sortOrder: $('#sortby option:selected').val() 
            },
            dataType:"html",
            type: "get",
            success: function(data){
                $(".shop-product-pagination").html(data);
            }
        });
    });
    
    $('#sortby').change(function (){
        $.ajax({
            url: "Products/Get",//this.value, //"Products/Index"
            data: { 
                pageSize: $('#select-showing option:selected').val(), 
                sortOrder: $('#sortby option:selected').val() 
            },
            dataType:"html",
            type: "get",
            success: function(data){
                $(".shop-product-pagination").html(data);
            }
        });
    })
});


