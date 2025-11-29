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
	'<div class="box9"><p class="boxT1 boxTW0 boxTC0 boxLH0">Работник</p>', 
	aoc2='<p class="boxT1 boxTW0 boxTC0 boxLH0">Телефон работника</p>', 
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
	'<p class="boxT1 boxTW0 boxTC0 boxLH0">Работник</p>',
	foc='<div class="boxO0"></div><p class="boxT1 boxTW1 boxLH0">',
	foe = '</p></div></div></div>';

let idOrder, idOrder1 = [], idOrder2 = [];
let orderHTML = '', orderHTML1 = '', orderHTML2 = '';

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
	orderHTML2 = '';
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

async function init()
{
	await tableCheakOrder();
	await tableCheakOrderFull1();
}

init();