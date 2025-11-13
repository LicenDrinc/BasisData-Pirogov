const bO1='<div class="boxO1"></div>', bO2='<div class="boxO2"></div>';
const bO3='<div class="boxO3"></div>', b2s='<div class="box2">', b2e='</div>'
const bps='<p class="boxT2">', bpe='</p>';
const bis='<input type="', bic='" id="', bie='">';
const bss='<select id="', bsc='">', bse='</select>';
const bos='<option value="', boc='">', boe='</option>';
const bts='<textarea type="', btc='" id="', bte='" ></textarea>';
const aos='<div class="box14" id="test"><div class="box15">'+
			'<div class="box1"><div class="box2">'+
			'<div class="box9"><p class="boxT1 boxTW0 boxTC0 boxLH0">Клиент</p>'+
			'<p class="boxT1 boxTW0 boxLH0">', 
			aoc1='</p></div><div class="box9">'+
			'<p class="boxT1 boxTW0 boxTC0 boxLH0">Телефон клиент</p>'+
			'<p class="boxT1 boxTW0 boxLH0">', 
			aoc2='</p></div><div class="box16">'+
			'<p class="boxT1 boxTW2 boxTC0 boxLH0">Объект</p>'+
			'<p class="boxT1 boxTW2 boxLH0">', 
			aoc3='</p></div><div class="box9">'+
			'<p class="boxT1 boxTW0 boxTC0 boxLH0">Поломка</p>'+
			'<p class="boxT1 boxTW0 boxLH0">', 
			aoc4='</p></div><div class="boxO0"></div><div class="box9">'+
			'<p class="boxT1 boxTW0 boxTC0 boxLH0">Дата</p>'+
			'<p class="boxT1 boxTW0 boxLH0">', 
			aoc5='</p></div><div class="box9">'+
			'<p class="boxT1 boxTW0 boxTC0 boxLH0">Цена</p>'+
			'<p class="boxT1 boxTW0 boxLH0">', 
			aoc6='</p></div><div class="box9">'+
			'<p class="boxT1 boxTW0 boxTC0 boxLH0">Статус</p>'+
			'<p class="boxT1 boxTW0 boxLH0">', 
			aoc7='</p></div><div class="box3 boxTW1">'+
			'<p class="boxT1 boxTW0 boxTC0 boxLH0">Слова клиента</p>'+
			'<div class="boxO0"></div><p class="boxT1 boxTW1 boxLH0">', 
			aoc8='</p></div></div></div>'+
			'<div class="box18"><div class="box19">'+
			'<div class="box17">'+
			'<form id="detailsOrder', 
			aoc9='"><button type="submit"><div class="box11">'+
			'<p class="boxT1">Подробние</p>'+
			'</div></button></form></div></div></div><div id="', 
			aoe='"></div></div></div>';

let tableObject; let tableClient; 
let tableWorker; let tableHuman;
let i11 = ''; let i12 = '';
let i21 = ''; let i22 = '';
let i31 = ''; let i32 = '';
let i41 = '';
let idOrder;

async function tableCheakOrder()
{
	let formData = new FormData();
	formData.append('id', localStorage.getItem("id_user"));
	const response = await fetch('serverCheakIdOrder.php', { body: formData, method: 'POST' });
	const result = await response.json();
	idOrder = result.object;
	for (let i = 0; i < idOrder.length; i++)
	{
		let formData = new FormData();
		formData.append('id', idOrder[i]);
		let response1 = await fetch('serverCheakOrder.php', { body: formData, method: 'POST' });
		let result1 = await response1.json();
		
	}
}


