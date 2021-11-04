function Edit(id) {
    //düzenleme butonuna basıldığında tablo satırındaki veriler modal da ilgili yerlere yerleştiriliyor
    document.getElementById("campaingName").value = $("#tr" + id + " td").eq(2).html();
    document.getElementById("campaingDescription").value = $("#tr" + id + " td").eq(3).html();
    document.getElementById("campaingPrice").value = $("#tr" + id + " td").eq(4).html();
    //düzenleme yapıldığında kamopanya id sini camğaing id alanına yazıyoruz.
    //bu sayede modal açıldığında campaingId dolu ise güncelleme yapılacaktır
    document.getElementById("baseCampaingId").value = $("#tr" + id + " td").eq(1).html();

    //Düzenlemeye Girildiğinde seçili olan tarife otomatik işaretleniyor
    let listSchedule = $("#tr" + id + " td").eq(5).html();
    const array = listSchedule.split("-");
    console.log(array);
    array.forEach(item => {
        $("#scheduleList input[type='radio']").each(function (index, element) {
            if (item == $(element).attr("data-name")) {// radiobuton un adını data-name yazmıştık
                $(element).attr("checked", true);
            }
        });
    });

    //Düzenlemeye Girildiğinde seçili olan ürünler otomatik işaretleniyor
    let listProduct = $("#tr" + id + " td").eq(6).html();
    const array1 = listProduct.split("-");
    console.log(array1);
    array1.forEach(item => {
        $("#productList input[type='checkbox']").each(function (index, element) {
            if (item == $(element).attr("data-name")) {// checkbox un adını data-name yazmıştık
                $(element).attr("checked", true);
            }
        });
    });

}

//Kampanya silme fonksiyonu
function Delete(id) {
    $.ajax({
        url: "/Campaing/DeleteCampaing",
        type: "Post",
        data: { campaingId: id },
    }).done(function (response) {
        console.log(response);
        if (response == "200") {
            $("#tr" + id + "").hide(200, function () {
                $("#tr" + id + "").remove();
            });
        } else {
            alert("Kampanya Silinirken Hata oluştu");
        }
    });

}

//Kampanya ekle butonuna tıklandığında modaldaki güncelle butonu kapatılıyor ekle butonu açılıyor
$("#btnModalAdd").click(function () {
    document.getElementById("btnUpdate").style.display = "none";
    document.getElementById("btnAdd").style.display = "block";
});

$(document).ready(function () {
    //Sayfa Yüklendiğin de kayıtlı olan kampanyalar otomatik olarak getiliyor ve listeleniyor
    $.ajax({
        url: '/Campaing/CampaingAndProductList',
        type: "Post",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response != "404") {
                response.campaings.forEach((item, index) => {
                    var tr = document.createElement("tr");
                    tr.id = "tr" + item.campaingId;
                    var Setting = document.createElement("td");

                    //Kampanya düzenleme butonu start

                    var btnEdit = document.createElement("i");
                    btnEdit.className = "text-warning bi bi-pencil-square fs-3";
                    btnEdit.id = item.campaingId;
                    btnEdit.setAttribute("data-bs-toggle", "modal");
                    btnEdit.setAttribute("data-bs-target", "#exampleModal");
                    Setting.append(btnEdit);

                    btnEdit.onclick = function () {
                        Edit(this.id);
                    };
                    //Kampanya düzenleme butonu End

                    //Kampanya silme butonu start

                    var btnDelete = document.createElement("i");
                    btnDelete.className = "text-danger bi bi-trash-fill fs-3";
                    btnDelete.id = item.campaingId;
                    Setting.append(btnDelete);

                    btnDelete.onclick = function () {
                        Delete(this.id);
                    };
                    //Kampanya silme butonu End

                    var tdId = document.createElement("td");
                    tdId.innerText = item.campaingId;
                    var tdCampaingName = document.createElement("td");
                    tdCampaingName.innerText = item.campaingName;
                    var tdCampaingDescription = document.createElement("td");
                    tdCampaingDescription.innerText = item.campaingDescription;
                    var tdPrice = document.createElement("td");
                    tdPrice.innerText = item.price;
                    var tdScheduleName = document.createElement("td");
                    var tdProductName = document.createElement("td");

                    response.schedules.forEach(item1 => {
                        if (item1.scheduleId == item.scheduleId) {

                            tdScheduleName.innerText = item1.name;
                            tdScheduleName.id = item1.scheduleId;
                        }
                    });

                    response.campaingAndProducts.forEach(item1 => {
                        if (item1.campaingId == item.campaingId) {
                            response.products.forEach(item2 => {
                                if (item2.productId == item1.productId) {

                                    tdProductName.innerText += item2.name + "-";
                                    tdProductName.id = item2.productId;
                                }
                            });
                        }
                    });
                    tr.append(Setting);
                    tr.append(tdId);
                    tr.append(tdCampaingName);
                    tr.append(tdCampaingDescription);
                    tr.append(tdPrice);
                    tr.append(tdScheduleName);
                    tr.append(tdProductName);

                    document.getElementById("tableBody").append(tr);
                });



            } else {
                alert("Kampanya Eklenemedi");
            }
        },
        error: function (err) {
        }
    });


    $.ajax({
        url: '/Schedule/ScheduleList',
        type: "Post",
        success: function (response) {
            console.log(response);
            if (response.success == "true") {
                for (const item of response.data.reverse()) {
                    var li = document.createElement("li");
                    li.id = item.scheduleId;

                    var label = document.createElement("label");
                    label.className = "dropdown-item";
                    var input = document.createElement("input");
                    input.type = "radio";
                    input.name = "schedule";
                    input.id = item.scheduleId;
                    input.setAttribute("data-name", item.name);
                    var span = document.createElement("span");
                    span.innerText = item.name;
                    span.style.marginLeft = "5px";
                    label.append(input);
                    label.append(span);
                    li.append(label);

                    document.getElementById("scheduleList").appendChild(li);
                }

            } else {
                alert("Tarife Bulunamadı");
            }
        },
        error: function (err) {
        }
    });

    $.ajax({
        url: '/Product/ProductList',
        type: "Post",
        success: function (response) {
            console.log(response);
            if (response.success == "true") {
                for (const item of response.data.reverse()) {
                    var li = document.createElement("li");
                    li.id = item.scheduleId;

                    var label = document.createElement("label");
                    label.className = "dropdown-item";
                    var input = document.createElement("input");
                    input.type = "checkbox";
                    input.id = item.productId;
                    input.setAttribute("data-name", item.name);
                    var span = document.createElement("span");
                    span.innerText = item.name;
                    span.style.marginLeft = "5px";

                    label.append(input);
                    label.append(span);
                    li.append(label);

                    document.getElementById("productList").appendChild(li);
                }

            } else {
                alert("Ürün Bulunamadı");
            }
        },
        error: function (err) {
        }
    });
});



$("#btnAdd").click(function () {
    var name = document.getElementById("campaingName").value;
    var description = document.getElementById("campaingDescription").value;
    var price = document.getElementById("campaingPrice").value;
    var scheduleId;
    $("#scheduleList input[type='radio']").each(function (index, element) {
        if ($(element).is(":checked") == true) {
            scheduleId = $(element).attr("id");
        }
    });

    var campaingModel, url, url1;
    if (document.getElementById("baseCampaingId").value == "") {
        //Ekleme ise modelin id sini eklemiyoruz. urlleri ayarlıyoruz
        url = "/Campaing/AddCampaing";
        url1 = "/Campaing/AddCampaingAndProduct";

        campaingModel = {
            CampaingName: name,
            CampaingDescription: description,
            ScheduleId: scheduleId,
            Price: price
        }
    } else {
        //Güncelleme ise url leri ayarlıyoruz. ve modelin id sini ekliyoruz
        url = "/Campaing/UpdateCampaing";
        url1 = "/Campaing/UpdateCampaingAndProduct";

        campaingModel = {
            CampaingId: $("#baseCampaingId").val(),
            CampaingName: name,
            CampaingDescription: description,
            ScheduleId: scheduleId,
            Price: price
        }
    }
    var sayac = 0;
    $("#productList input[type='checkbox']").each(function (index, element) {
        if ($(element).is(":checked") == true) {
            sayac++;
        }
    });
    if (isNaN(campaingModel.Price) == false && sayac > 0 && campaingModel.CampaingName.toString().trim() != "" && campaingModel.CampaingDescription.toString().trim() != "" && campaingModel.ScheduleId.toString().trim() != "" && campaingModel.Price.toString().trim() != "") {
        //Tüm alanlar dolu ise işlemler yapılıyor
        $.ajax({
            url: url,
            type: "Post",
            dataType: "json",
            data: campaingModel,
            success: function (response) {
                console.log(response);
                if (response.success == "true") {
                    var sayac = 0;
                    $("#productList input[type='checkbox']").each(function (index, element) {
                        if ($(element).is(":checked") == true) {
                            productId = $(element).attr("id");

                            var campaingAndProduct = {
                                CampaingId: response.data.campaingId,
                                ProductId: productId
                            }
                            $.ajax({
                                url: url1,
                                type: "Post",
                                dataType: "json",
                                data: campaingAndProduct,
                                success: function (response1) {
                                    console.log(response1);
                                    if (response1.success == "false") {
                                        sayac++;
                                    }
                                },
                                error: function (err1) {
                                }
                            });
                        }
                    });

                    function endResponse() { 

                    }
                    if (sayac == 0) {
                        alert("Kampaya Başarıyla Eklendi");
                        window.location.reload();
                    } else {
                        alert("Kampanya Eklenirken Bir Hata Oluştu. Bazı Ürünler Eklenemedi");
                    }



                } else {
                    alert("Kampanya Eklenemedi");
                }
            },
            error: function (err) {
            }
        });


    } else alert("Lütfen Tüm Alanları Eksiksiz Doldurunuz");

});
