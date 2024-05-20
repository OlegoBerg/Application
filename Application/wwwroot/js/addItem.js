document.getElementById('addItem').addEventListener('click', function () {
    var itemsContainer = document.getElementById('items');
    var newItem = document.createElement('div');
    var index = itemsContainer.children.length;
    newItem.innerHTML =
        '<label>Название товара:</label>' +
        '<input type="text" name="Items[' + index + '].Name" /><br />' +
        '<label>Цена:</label>' +
        '<input type="text" name="Items[' + index + '].Price" /><br />';
    itemsContainer.appendChild(newItem);
});