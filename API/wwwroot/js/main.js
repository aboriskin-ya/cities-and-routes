document.addEventListener("DOMContentLoaded", function (event) {
    // получаем список всех изображений
    fetch('/image/getall')
        .then(response => response.json())
        .then(result => {
            result.forEach(element => document.getElementById('imageList2').insertAdjacentHTML('beforeend', '<option value="' + element.id + '">' + element.id + '</option>'));
        })
    document.getElementById('imageList2').addEventListener('change', function () {
        var mapForm = document.getElementById('imageGet');
        mapForm.setAttribute('action', mapForm.dataset.action.replace('{id}', this.value));
    });
    // получаем список всех Карт
    fetch('/map/getall')
        .then(response => response.json())
        .then(result => {
            result.forEach(element => document.getElementById('mapList').insertAdjacentHTML('beforeend', '<option value="' + element.id + '">' + element.name + '</option>'));
        })
    document.getElementById('mapList').addEventListener('change', function () {
        var mapForm = document.getElementById('mapGet');
        mapForm.setAttribute('action', mapForm.dataset.action.replace('{id}', this.value));
    });

    fetch('/settings/getall')
        .then(response => response.json())
        .then(result => {
            result.forEach(element => document.getElementById('settingsList').insertAdjacentHTML('beforeend', '<option value="' + element.id + '">' + element.id + '</option>'));
        })
    document.getElementById('settingsList').addEventListener('change', function () {
        var mapForm = document.getElementById('settingsGet');
        mapForm.setAttribute('action', mapForm.dataset.action.replace('{id}', this.value));
    });
})