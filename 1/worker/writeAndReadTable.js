const bO1='<div class="boxO1"></div>', bO2='<div class="boxO2"></div>';
const bO3='<div class="boxO3"></div>', b2s='<div class="box2">', b2e='</div>'
const bps='<p class="boxT2">', bpe='</p>';
const bis='<input type="', bic='" id="', bie='">';
const bss='<select id="', bsc='">', bse='</select>';
const bos='<option value="', boc='">', boe='</option>';
const bts = '<textarea type="', btc = '" id="', bte = '" ></textarea>';
const oCon1 = '<p class="boxT1 boxTW0 boxLH0">', oCon2 = '</p></div><div class="box9">',
	oCon3 = '</p></div><div class="box3 boxTW1"><p class="boxT1 boxTW0 boxTC0 boxLH0">Слова клиента</p>';
const aos='<div class="box14" id="order', 
	aoc1='"><div class="box15"><div class="box1"><div class="box2">'+
	'<div class="box9"><p class="boxT1 boxTW0 boxTC0 boxLH0">Клиент</p>', 
	aoc2='<p class="boxT1 boxTW0 boxTC0 boxLH0">Телефон клиент</p>', 
	aoc3='</p></div><div class="box16">'+
	'<p class="boxT1 boxTW2 boxTC0 boxLH0">Объект</p>'+
	'<p class="boxT1 boxTW2 boxLH0">', 
	aoc4='<p class="boxT1 boxTW0 boxTC0 boxLH0">Поломка</p>', 
	aoc5='</p></div><div class="boxO0"></div><div class="box9">'+
	'<p class="boxT1 boxTW0 boxTC0 boxLH0">Дата</p>', 
	aoc6='<p class="boxT1 boxTW0 boxTC0 boxLH0">Цена</p>', 
	aoc7='<p class="boxT1 boxTW0 boxTC0 boxLH0">Статус</p>', 
	aoc8='<div class="boxO0"></div><p class="boxT1 boxTW1 boxLH0">', 
	aoc9='</p></div></div></div><div class="box18"><div class="box19">'+
	'<div class="box17"><form id="detailsOrder', 
	aoc10='"><button type="submit"><div class="box11"><p class="boxT1">Подробнее</p>'+
	'</div></button></form></div></div></div><div class="box22" id="full',
	aoe='"></div></div></div>';
const fos='<div class="box20"><div class="box21"><div class="box9">'+
	'<p class="boxT1 boxTW0 boxTC0 boxLH0">Клиент</p>',
	foc='<div class="boxO0"></div><p class="boxT1 boxTW1 boxLH0">',
	foe = '</p></div></div></div>';
const pos='<div class="box1"><div class="box2"><div class="box9">' +
	'<p class="boxT0">Поломка</p><input type="text" id="failure',
	poc1='" /></div><div class="box9"><p class="boxT0">Дата</p><input type="date" id="date',
	poc2='" /></div><div class="box9"><p class="boxT0">Цена</p><input type="text" id="price',
	poc3='" /></div><div class="box9"><p class="boxT0">Статус</p><select id="status',
	poc4='"><option value="новый">Новый</option><option value="диагностика">Диагностика</option>' +
	'<option value="ремонт">Ремонт</option><option value="выдано">Выдано</option>' +
	'<option value="готова к выдаче">Готова к выдаче</option><option value="не подлежит ремонту">Не подлежит ремонту</option>' +
	'</select></div></div></div><div class="box6"><div class="box7"><div class="box10"><form id="nextOrder',
	poc5 = '" onsubmit="event.preventDefault(); updateOrder(',
	poc6 = ');">' +
	'<button type="submit"><div class="box11"><p class="boxT4">Обновить данные заказа</p>'+
	'</div></button></form></div><div id="result',
	poe='"></div></div></div>';

let tableObject; let tableClient; 
let tableWorker; let tableHuman;
let i11 = ''; let i12 = '', i21 = ''; let i22 = '';
let i31 = ''; let i32 = '', i41 = '';
let idOrder, idOrder1 = [], idOrder2 = [];
let orderHTML = '', orderHTML1 = '', orderHTML2 = '';

async function tableSpecialChek() {
	i11 = i12 = i21 = i22 = i31 = i32 = i41 = bos + 0 + boc + 'не указана' + boe;
	const response = await fetch('serverClientSpecial.php', {
		method: 'POST'
	});
	const result = await response.json();
	tableObject = result.object; tableClient = result.client;
	tableWorker = result.worker; tableHuman = result.human;

	for (let i = 0; i < tableHuman.length; i++) {
		i11 += bos + tableHuman[i][0] + boc + tableHuman[i][1] + boe;
		i12 += bos + tableHuman[i][0] + boc + tableHuman[i][2] + boe;
		let c = 0; let c1 = 0;
		for (let j = 0; j < tableClient.length; j++)
			if (tableHuman[i][0] == tableClient[j][1]) c = 1;
		for (let j = 0; j < tableWorker.length; j++)
			if (tableHuman[i][0] == tableWorker[j][1]) c1 = 1;
		if (c == 1) {
			i21 += bos + tableHuman[i][0] + boc + tableHuman[i][1] + boe;
			i22 += bos + tableHuman[i][0] + boc + tableHuman[i][2] + boe;
		}
		if (c1 == 1) {
			i31 += bos + tableHuman[i][0] + boc + tableHuman[i][1] + boe;
			i32 += bos + tableHuman[i][0] + boc + tableHuman[i][2] + boe;
		}
	}
	for (let i = 0; i < tableObject.length; i++)
		i41 += bos + tableObject[i][0] + boc + tableObject[i][1] + boe;

	objectId();
}

async function tableCheakOrder()
{
	orderHTML = ''; orderHTML1 = '';
	let formData = new FormData();
	formData.append('id', localStorage.getItem("id_user"));
	const response = await fetch('serverCheakIdOrder.php', { body: formData, method: 'POST' });
	const result = await response.json();
	idOrder = result.object;
	for (let i = 0; i < idOrder.length; i++)
	{
		idOrder1[i] = 1;
		let formData = new FormData();
		formData.append('id', idOrder[i]);
		formData.append('mode', 0);
		let response1 = await fetch('serverCheakOrder.php', { body: formData, method: 'POST' });
		let result1 = await response1.json();
		if (result1.object[7] == "выдано")
			orderHTML1 += aos + idOrder[i] + aoc1 + oCon1 + result1.object[0] + oCon2 + aoc2 + oCon1 + result1.object[1] + 
				aoc3 + result1.object[4] + oCon2 + aoc4 + oCon1 + result1.object[3] + aoc5 + oCon1 + result1.object[2] +
				oCon2 + aoc6 + oCon1 + result1.object[5] + oCon2 + aoc7 + oCon1 + result1.object[7] + oCon3 + aoc8 + result1.object[6] +
				aoc9 + idOrder[i] + aoc10 + idOrder[i] + aoe;
		else
			orderHTML += aos + idOrder[i] + aoc1 + oCon1 + result1.object[0] + oCon2 + aoc2 + oCon1 + result1.object[1] + 
				aoc3 + result1.object[4] + oCon2 + aoc4 + oCon1 + result1.object[3] + aoc5 + oCon1 + result1.object[2] +
				oCon2 + aoc6 + oCon1 + result1.object[5] + oCon2 + aoc7 + oCon1 + result1.object[7] + oCon3 + aoc8 + result1.object[6] + 
				aoc9 + idOrder[i] + aoc10 + idOrder[i] + aoe;
		idOrder2[i] = result1.object[7] == 'выдано' ? 1 : 0;
	}
	document.getElementById('activeOrders').innerHTML = orderHTML;
	document.getElementById('activeOrders1').innerHTML = orderHTML1;
}

async function tableCheakOrderFull(x, y)
{
	let formData = new FormData();
	formData.append('id', x);
	formData.append('mode', 1);
	const response = await fetch('serverCheakOrder.php', { body: formData, method: 'POST' });
	const result = await response.json();
	if (y == 0) orderHTML2 = pos + x + poc1 + x + poc2 + x + poc3 + x + poc4 + x + poc5 + x + poc6 + x + poe;
	else orderHTML2 = '';
	for (let i = result.object.length - 2; i >= 0; i--)
	{
		orderHTML2 += fos + oCon1 + result.object[i][0] + oCon2 + aoc2 + oCon1 + result.object[i][1] +
			aoc3 + result.object[i][4] + oCon2 + aoc4 + oCon1 + result.object[i][3] + aoc5 + oCon1 + result.object[i][2] +
			oCon2 + aoc6 + oCon1 + result.object[i][5] + oCon2 + aoc7 + oCon1 + result.object[i][7] + oCon3 + foc + result.object[i][6] + foe;
	}
	document.getElementById('full' + x).innerHTML = orderHTML2;
}

async function tableCheakOrderFull1()
{
	for (let i = 0; i < idOrder.length; i++)
	{
		document.getElementById("detailsOrder" + idOrder[i]).addEventListener('submit', async function (e) {
			e.preventDefault();
			idOrder1[i] = idOrder1[i] == 0 ? 1 : 0;
			if (idOrder1[i] == 0) tableCheakOrderFull(idOrder[i], idOrder2[i]);
			else document.getElementById('full' + idOrder[i]).innerHTML = '';
		});
		if (idOrder1[i] == 0) tableCheakOrderFull(idOrder[i], idOrder2[i]);
		else document.getElementById('full' + idOrder[i]).innerHTML = '';
	}
}

function objectId()
{
	document.getElementById('id_object').innerHTML = i41;
}

async function newOrder()
{
	document.getElementById('newOrder').addEventListener('submit', async function (e) {
		e.preventDefault();

		const formData = new FormData();
		formData.append('name_client', document.getElementById('nameClient').value);
		formData.append('phone_client', document.getElementById('phoneClient').value);
		formData.append('id_worker', localStorage.getItem("id_user"));
		formData.append('date', document.getElementById('date').value);
		formData.append('id_object', document.getElementById('id_object').value);
		formData.append('newObject', document.getElementById('newObject').value);
		formData.append('price', document.getElementById('price').value);
		formData.append('clients_words', document.getElementById('clients_words').value);

		const response = await fetch('serverNewOrder.php', {
			method: 'POST',
			body: formData,
		});

		const result = await response.text();
		document.getElementById('result').innerHTML = result;
		tableSpecialChek();
		tableCheakOrder();
		tableCheakOrderFull1();
	});
}

async function updateOrder(x)
{
	const formData = new FormData();
	formData.append('id', x);
	formData.append('failure', document.getElementById('failure'+x).value);
	formData.append('price', document.getElementById('price'+x).value);
	formData.append('status', document.getElementById('status'+x).value);
	formData.append('date', document.getElementById('date'+x).value);

	const response = await fetch('serverUpdateOrder.php', {
		method: 'POST',
		body: formData,
	});

	const result = await response.text();
	document.getElementById('result').innerHTML = result;
	tableSpecialChek();
	tableCheakOrder();
	tableCheakOrderFull1();
}

async function init()
{
	await tableSpecialChek();
	await newOrder();
	await tableCheakOrder();
	await tableCheakOrderFull1();
}

init();