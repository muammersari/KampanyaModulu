//Tarife ekle butonuna tıklandığında modaldaki güncelle butonu kapatılıyor ekle butonu açılıyor
$("#btnModalAdd").click(function () {
    document.getElementById("btnUpdate").style.display = "none";
    document.getElementById("btnAdd").style.display = "block";
});

function Edit(id) {
    //düzenleme butonuna basıldığında tablo satırındaki veriler modal da ilgili yerlere yerleştiriliyor
    document.getElementById("scheduleId").value = $("#tr" + id + " td").eq(1).html();
    document.getElementById("scheduleName").value = $("#tr" + id + " td").eq(2).html();
    document.getElementById("scheduleKdv").value = $("#tr" + id + " td").eq(3).html();
    document.getElementById("scheduleOiv").value = $("#tr" + id + " td").eq(4).html();

    //Tarife düzünleme butonuna tıklandığında modaldaki güncelle butonu açılıyor ekle butonu kapatılıyor
    document.getElementById("btnUpdate").style.display = "block";
    document.getElementById("btnAdd").style.display = "none";
}
function Delete(id) {
    $.ajax({
        url: "/Schedule/DeleteSchedule",
        type: "Post",
        data: { scheduleId: id },
    }).done(function (response) {
        console.log(response);
        if (response == "200") {

            $("#tr" + id + "").hide(200, function () {
                $("#tr" + id + "").remove();
            });
        } else {
            alert("Tarife Silinirken Hata oluştu");
        }
    });

}

$(document).ready(function () {
    //Sayfa yüklendiğinde  tüm Tarifeler listeleniyor
    $.ajax({
        url: '/Schedule/ScheduleList',
        type: "Post",
        success: function (response) {
            console.log(response);
            if (response.success == "true") {
                for (const item of response.data.reverse()) {
                    var tr = document.createElement("tr");
                    tr.id = "tr" + item.scheduleId;

                    var Setting = document.createElement("td");

                    //Tarife düzenleme butonu start

                    var btnEdit = document.createElement("i");
                    btnEdit.className = "text-warning bi bi-pencil-square fs-3";
                    btnEdit.id = item.scheduleId;
                    btnEdit.setAttribute("data-bs-toggle", "modal");
                    btnEdit.setAttribute("data-bs-target", "#exampleModal");
                    Setting.append(btnEdit);

                    btnEdit.onclick = function () {
                        Edit(this.id);
                    };
                    //Tarife düzenleme butonu End

                    //Tarife silme butonu start

                    var btnDelete = document.createElement("i");
                    btnDelete.className = "text-danger bi bi-trash-fill fs-3";
                    btnDelete.id = item.scheduleId;
                    Setting.append(btnDelete);

                    btnDelete.onclick = function () {
                        Delete(this.id);
                    };
                    //Tarife silme butonu End

                    var scheduleId = document.createElement("td");
                    scheduleId.innerText = item.scheduleId;

                    var name = document.createElement("td");
                    name.innerText = item.name;

                    var KDV = document.createElement("td");
                    KDV.innerText = item.kdv;

                    var OIV = document.createElement("td");
                    OIV.innerText = item.oiv;

                    tr.append(Setting);
                    tr.append(scheduleId);
                    tr.append(name);
                    tr.append(KDV);
                    tr.append(OIV);
                    //tablonun body kısmına satır ekleniyor
                    document.getElementById("tableBody").append(tr);
                }


            } else {
                alert("Tarife Bulunamadı");
            }
        },
        error: function (err) {
        }
    });
});


$("#AddScheduleForm").submit(function (event) {
    //Tarife eklendiğinde tabloya ekliyor
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
            //Eğer Tarife de ekleme yapıldı ise yeni satır ekleniyor
            if (clickButton == "Ekle") {
                alert("Tarife Başarıyla Eklendi");

                var tr = document.createElement("tr");
                tr.id = "tr" + response.data.scheduleId;
                var Setting = document.createElement("td");

                //Tarife düzenleme butonu start

                var btnEdit = document.createElement("i");
                btnEdit.className = "text-warning bi bi-pencil-square fs-3";
                btnEdit.id = response.data.scheduleId;
                btnEdit.setAttribute("data-bs-toggle", "modal");
                btnEdit.setAttribute("data-bs-target", "#exampleModal");
                Setting.append(btnEdit);

                btnEdit.onclick = function () {
                    Edit(this.id);
                };
                //Tarife düzenleme butonu End

                //Tarife silme butonu start

                var btnDelete = document.createElement("i");
                btnDelete.className = "text-danger bi bi-trash-fill fs-3";
                btnDelete.id = response.data.scheduleId;
                Setting.append(btnDelete);

                btnDelete.onclick = function () {
                    Delete(this.id);
                };
                //Tarife silme butonu End

                var scheduleId = document.createElement("td");
                scheduleId.innerText = response.data.scheduleId;

                var name = document.createElement("td");
                name.innerText = response.data.name;

                var KDV = document.createElement("td");
                KDV.innerText = response.data.kdv;

                var OIV = document.createElement("td");
                OIV.innerText = response.data.oiv;

                tr.append(Setting);
                tr.append(scheduleId);
                tr.append(name);
                tr.append(KDV);
                tr.append(OIV);
                //prepend = tablonun ilk sırasında ekleme yapar
                document.getElementById("tableBody").prepend(tr);
            } else {
                //Eğer Tarife de güncelleme yapıldı ise güncellenen satırım değerleri güncelleniyor
                alert("Tarife Başarıyla Güncellendi");
                $("#tr" + response.data.scheduleId + " td").eq(1).html(response.data.scheduleId);
                $("#tr" + response.data.scheduleId + " td").eq(2).html(response.data.name);
                $("#tr" + response.data.scheduleId + " td").eq(3).html(response.data.kdv);
                $("#tr" + response.data.scheduleId + " td").eq(4).html(response.data.oiv);

                //window.location.reload();
            }

        } else {
            alert("Tarife Eklenirken Hata oluştu");
        }
    });
});