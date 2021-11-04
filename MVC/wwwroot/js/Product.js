//Ürün ekle butonuna tıklandığında modaldaki güncelle butonu kapatılıyor ekle butonu açılıyor
$("#btnModalAdd").click(function () {
    document.getElementById("btnUpdate").style.display = "none";
    document.getElementById("btnAdd").style.display = "block";
});

function Edit(id) {
    //düzenleme butonuna basıldığında tablo satırındaki veriler modal da ilgili yerlere yerleştiriliyor
    document.getElementById("productId").value = $("#tr" + id + " td").eq(1).html();
    document.getElementById("productName").value = $("#tr" + id + " td").eq(2).html();
    document.getElementById("productKdv").value = $("#tr" + id + " td").eq(3).html();
    document.getElementById("productAmout").value = $("#tr" + id + " td").eq(4).html();

    //Ürün düzünleme butonuna tıklandığında modaldaki güncelle butonu açılıyor ekle butonu kapatılıyor
    document.getElementById("btnUpdate").style.display = "block";
    document.getElementById("btnAdd").style.display = "none";
}
function Delete(id) {
    $.ajax({
        url: "/Product/DeleteProduct",
        type: "Post",
        data: { productId: id },
    }).done(function (response) {
        console.log(response);
        if (response == "200") {

            $("#tr" + id + "").hide(200, function () {
                $("#tr" + id + "").remove();
            });
        } else {
            alert("Ürün Silinirken Hata oluştu");
        }
    });

}

$(document).ready(function () {
    //Sayfa yüklendiğinde  tüm ürünler listeleniyor
    $.ajax({
        url: '/Product/ProductList',
        type: "Post",
        success: function (response) {
            console.log(response);
            if (response.success == "true") {
                for (const item of response.data.reverse()) {
                    var tr = document.createElement("tr");
                    tr.id = "tr" + item.productId;

                    var Setting = document.createElement("td");

                    //Ürün düzenleme butonu start

                    var btnEdit = document.createElement("i");
                    btnEdit.className = "text-warning bi bi-pencil-square fs-3";
                    btnEdit.id = item.productId;
                    btnEdit.setAttribute("data-bs-toggle", "modal");
                    btnEdit.setAttribute("data-bs-target", "#exampleModal");
                    Setting.append(btnEdit);

                    btnEdit.onclick = function () {
                        Edit(this.id);
                    };
                    //Ürün düzenleme butonu End

                    //Ürün silme butonu start

                    var btnDelete = document.createElement("i");
                    btnDelete.className = "text-danger bi bi-trash-fill fs-3";
                    btnDelete.id = item.productId;
                    Setting.append(btnDelete);

                    btnDelete.onclick = function () {
                        Delete(this.id);
                    };
                    //Ürün silme butonu End

                    var productId = document.createElement("td");
                    productId.innerText = item.productId;

                    var name = document.createElement("td");
                    name.innerText = item.name;

                    var KDV = document.createElement("td");
                    KDV.innerText = item.kdv;

                    var Amout = document.createElement("td");
                    Amout.innerText = item.amout;

                    tr.append(Setting);
                    tr.append(productId);
                    tr.append(name);
                    tr.append(KDV);
                    tr.append(Amout);
                    //tablonun body kısmına satır ekleniyor
                    document.getElementById("tableBody").append(tr);
                }


            } else {
                alert("Ürün Bulunamadı");
            }
        },
        error: function (err) {
        }
    });
});


$("#AddProductForm").submit(function (event) {
    //Ürün eklendiğinde tabloya ekliyor
    event.preventDefault(); //burada tıklanınca post işlemi yapmasın diye önlem alıyoruz
    var post_url = $(this).find("input[type=submit]:focus").attr("action"); //formun urlsi alınıyor
    var request_method = $(this).attr("method"); //formun metodu alınıyor
    var form_data = $(this).serialize(); //formun datası alınıyor
    var clickButton = $(this).find("input[type=submit]:focus").val();

    $.ajax({
        url: post_url,
        type: request_method,
        data: form_data
    }).done(function (response) {
        console.log(response);
        if (response.success == "true") {
            //Eğer üründe ekleme yapıldı ise yeni satır ekleniyor
            if (clickButton == "Ekle") {
                alert("Ürün Başarıyla Eklendi");

                var tr = document.createElement("tr");
                tr.id = "tr" + response.data.productId;
                var Setting = document.createElement("td");

                //Ürün düzenleme butonu start

                var btnEdit = document.createElement("i");
                btnEdit.className = "text-warning bi bi-pencil-square fs-3";
                btnEdit.id = response.data.productId;
                btnEdit.setAttribute("data-bs-toggle", "modal");
                btnEdit.setAttribute("data-bs-target", "#exampleModal");
                Setting.append(btnEdit);

                btnEdit.onclick = function () {
                    Edit(this.id);
                };
                //Ürün düzenleme butonu End

                //Ürün silme butonu start

                var btnDelete = document.createElement("i");
                btnDelete.className = "text-danger bi bi-trash-fill fs-3";
                btnDelete.id = response.data.productId;
                Setting.append(btnDelete);

                btnDelete.onclick = function () {
                    Delete(this.id);
                };
                //Ürün silme butonu End

                var productId = document.createElement("td");
                productId.innerText = response.data.productId;

                var name = document.createElement("td");
                name.innerText = response.data.name;

                var KDV = document.createElement("td");
                KDV.innerText = response.data.kdv;

                var Amout = document.createElement("td");
                Amout.innerText = response.data.amout;

                tr.append(Setting);
                tr.append(productId);
                tr.append(name);
                tr.append(KDV);
                tr.append(Amout);
                //prepend = tablonun ilk sırasında ekleme yapar
                document.getElementById("tableBody").prepend(tr);
            } else {
                //Eğer üründe güncelleme yapıldı ise güncellenen satırım değerleri güncelleniyor
                alert("Ürün Başarıyla Güncellendi");
                $("#tr" + response.data.productId + " td").eq(1).html(response.data.productId);
                $("#tr" + response.data.productId + " td").eq(2).html(response.data.name);
                $("#tr" + response.data.productId + " td").eq(3).html(response.data.kdv);
                $("#tr" + response.data.productId + " td").eq(4).html(response.data.amout);
            }

        } else {
            alert("Ürün Eklenirken Hata oluştu");
        }
    });
});