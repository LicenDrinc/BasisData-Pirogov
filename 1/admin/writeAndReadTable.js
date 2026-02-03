const bO1='<div class="boxO1"></div>', bO2='<div class="boxO2"></div>';
const bO3='<div class="boxO3"></div>', b2s='<div class="box2">', b2e='</div>'
const bps='<p class="boxT2">', bpe='</p>';
const bis='<input type="', bic='" id="', bie='">';
const bss='<select id="', bsc='">', bse='</select>';
const bos='<option value="', boc='">', boe='</option>';
const bts='<textarea type="', btc='" id="', bte='" ></textarea>';
const statusPlusCon = bos+'новый'+boc+'Новый'+boe+bos+'диагностика'+boc+'Диагностика'+boe+
					bos+'ремонт'+boc+'Ремонт'+boe+bos+'выдано'+boc+'Выдано'+boe+
					bos+'готова к выдаче'+boc+'Готова к выдаче'+boe+
					bos+'не подлежит ремонту'+boc+'Не подлежит ремонту'+boe;

let tableObject; let tableClient; 
let tableWorker; let tableHuman;
let i11 = ''; let i12 = '', i21 = ''; let i22 = '';
let i31 = ''; let i32 = '', i41 = '';
async function tableSpecialChek()
{
	i11 = i12 = i21 = i22 = i31 = i32 = i41 = bos+boc+'не указана'+boe;
	const response = await fetch('serverClientSpecial.php', {
		method: 'POST'
	});
	const result = await response.json();
	tableObject = result.object; tableClient = result.client;
	tableWorker = result.worker; tableHuman = result.human;
	
	for (let i = 0; i < tableHuman.length; i++)
	{
		i11 += bos+tableHuman[i][0]+boc+tableHuman[i][1]+boe;
		i12 += bos+tableHuman[i][0]+boc+tableHuman[i][2]+boe;
		let c = 0; let c1 = 0;
		for (let j = 0; j < tableClient.length; j++)
			if (tableHuman[i][0] == tableClient[j][1]) c = 1;
		for (let j = 0; j < tableWorker.length; j++)
			if (tableHuman[i][0] == tableWorker[j][1]) c1 = 1;
		if (c == 1)
		{
			i21 += bos+tableHuman[i][0]+boc+tableHuman[i][1]+boe;
			i22 += bos+tableHuman[i][0]+boc+tableHuman[i][2]+boe;
		}
		if (c1 == 1)
		{
			i31 += bos+tableHuman[i][0]+boc+tableHuman[i][1]+boe;
			i32 += bos+tableHuman[i][0]+boc+tableHuman[i][2]+boe;
		}
	}
	for (let i = 0; i < tableObject.length; i++)
		i41 += bos+tableObject[i][0]+boc+tableObject[i][1]+boe;

	tableChek(); tablePlusChek();
	return 0;
}

function tableChek()
{
	if (document.getElementById('type_table').value == "_order")
		document.getElementById('basisForConclusion').innerHTML = bO3+b2s+
		bps+'Имя клиента'+bpe+bO1+			bss+'name_client'+bsc+i21+bse+bO1+
		bps+'Телефон клиента'+bpe+bO1+		bss+'phone_client'+bsc+i22+bse+bO1+
		bps+'Имя работника'+bpe+bO1+		bss+'name_worder'+bsc+i31+bse+bO1+
		bps+'Телефон работника'+bpe+bO1+	bss+'phone_worder'+bsc+i32+bse+bO1+
		bps+'Дата'+bpe+bO1+					bis+'date'+bic+'date'+bie+bO1+
		bps+'Название объекта'+bpe+bO1+		bss+'id_object'+bsc+i41+bse+bO1+
		bps+'Номер заказа'+bpe+bO1+			bis+'number'+bic+'id_order'+bie+bO1+bO2+b2e;
	else if (document.getElementById('type_table').value == "object")
		document.getElementById('basisForConclusion').innerHTML = bO3+b2s+
		bps+'Название объекта'+bpe+bO1+		bss+'id_object'+bsc+i41+bse+bO1+bO2+b2e;
	else if (document.getElementById('type_table').value == "client")
		document.getElementById('basisForConclusion').innerHTML = bO3+b2s+
		bps+'Имя клиента'+bpe+bO1+			bss+'name_client'+bsc+i21+bse+bO1+
		bps+'Телефон клиента'+bpe+bO1+		bss+'phone_client'+bsc+i22+bse+bO1+bO2+b2e;
	else if (document.getElementById('type_table').value == "worker")
		document.getElementById('basisForConclusion').innerHTML = bO3+b2s+
		bps+'Имя работника'+bpe+bO1+		bss+'name_worder'+bsc+i31+bse+bO1+
		bps+'Телефон работника'+bpe+bO1+	bss+'phone_worder'+bsc+i32+bse+bO1+bO2+b2e;
	else if (document.getElementById('type_table').value == "human")
		document.getElementById('basisForConclusion').innerHTML = bO3+b2s+
		bps+'Имя'+bpe+bO1+					bss+'name'+bsc+i11+bse+bO1+
		bps+'Телефон'+bpe+bO1+				bss+'phone_number'+bsc+i12+bse+bO1+bO2+b2e;
	else document.getElementById('basisForConclusion').innerHTML = '';
	return 0;
}

function tablePlusChek()
{
	if (document.getElementById('type_table_1').value == "_order")
		document.getElementById('basisForConclusion1').innerHTML = bO3+b2s+
		bps+'Имя клиента'+bpe+bO1+			bss+'name_clientPlus'+bsc+i21+bse+bO1+
		bps+'Телефон клиента'+bpe+bO1+		bss+'phone_clientPlus'+bsc+i22+bse+bO1+
		bps+'Имя работника'+bpe+bO1+		bss+'name_worderPlus'+bsc+i31+bse+bO1+
		bps+'Телефон работника'+bpe+bO1+	bss+'phone_worderPlus'+bsc+i32+bse+bO1+
		bps+'Дата'+bpe+bO1+					bis+'date'+bic+'datePlus'+bie+bO1+
		bps+'Поломка'+bpe+bO1+				bis+'text'+bic+'failurePlus'+bie+bO1+
		bps+'Название объекта'+bpe+bO1+		bss+'id_objectPlus'+bsc+i41+bse+bO1+
		bps+'Цена'+bpe+bO1+					bis+'text'+bic+'pricePlus'+bie+bO1+
		bps+'Со слов клиента'+bpe+bO1+		bts+'text'+btc+'clients_wordsPlus'+bte+bO1+
		bps+'Статус'+bpe+bO1+				bss+'statusPlus'+bsc+statusPlusCon+bse+bO1+
		bps+'Номер заказа'+bpe+bO1+			bis+'number'+bic+'id_orderPlus'+bie+bO1+bO2+b2e;
	else if (document.getElementById('type_table_1').value == "object")
		document.getElementById('basisForConclusion1').innerHTML = bO3+b2s+
		bps+'Названия'+bpe+bO1+				bis+'text'+bic+'namePlus'+bie+bO1+bO2+b2e;
	else if (document.getElementById('type_table_1').value == "client")
		document.getElementById('basisForConclusion1').innerHTML = bO3+b2s+
		bps+'Имя клиента'+bpe+bO1+			bss+'name_clientPlus'+bsc+i11+bse+bO1+
		bps+'Телефон клиента'+bpe+bO1+		bss+'phone_clientPlus'+bsc+i12+bse+bO1+bO2+b2e;
	else if (document.getElementById('type_table_1').value == "worker")
		document.getElementById('basisForConclusion1').innerHTML = bO3+b2s+
		bps+'Имя работника'+bpe+bO1+		bss+'name_worderPlus'+bsc+i11+bse+bO1+
		bps+'Телефон работника'+bpe+bO1+	bss+'phone_worderPlus'+bsc+i12+bse+bO1+bO2+b2e;
	else if (document.getElementById('type_table_1').value == "human")
		document.getElementById('basisForConclusion1').innerHTML = bO3+b2s+
		bps+'Имя'+bpe+bO1+					bis+'text'+bic+'namePlus'+bie+bO1+
		bps+'Номер телефона'+bpe+bO1+		bis+'text'+bic+'phone_numberPlus'+bie+bO1+bO2+b2e;
	else document.getElementById('basisForConclusion1').innerHTML = '';
	return 0;
}

tableSpecialChek();

document.getElementById("type_table").addEventListener("click",() => {
	tableSpecialChek();
});

document.getElementById("type_table_1").addEventListener("click",() => {
	tableSpecialChek();
});

document.getElementById("writeTable").addEventListener("submit", async function (e) {
	e.preventDefault();
	
	const formData = new FormData();
	formData.append('type_table', document.getElementById('type_table').value);

	if (document.getElementById('type_table').value == '_order') {
		formData.append('date', document.getElementById('date').value);
		formData.append('name_client', document.getElementById('name_client').value);
		formData.append('phone_client', document.getElementById('phone_client').value);
		formData.append('name_worder', document.getElementById('name_worder').value);
		formData.append('phone_worder', document.getElementById('phone_worder').value);
		formData.append('id_object', document.getElementById('id_object').value);
		formData.append('id_order', document.getElementById('id_order').value);
	}
	if (document.getElementById('type_table').value == 'client') {
		formData.append('name_client', document.getElementById('name_client').value);
		formData.append('phone_client', document.getElementById('phone_client').value);
	}
	if (document.getElementById('type_table').value == 'worker') {
		formData.append('name_worder', document.getElementById('name_worder').value);
		formData.append('phone_worder', document.getElementById('phone_worder').value);
	}
	if (document.getElementById('type_table').value == 'human') {
		formData.append('phone_number', document.getElementById('phone_number').value);
		formData.append('name', document.getElementById('name').value);
	}
	if (document.getElementById('type_table').value == 'object') {
		formData.append('id_object', document.getElementById('id_object').value);
	}
	
	const response = await fetch('serverWriteTable.php', {
		method: 'POST',
		body: formData,
	});
	const result = await response.text();
	document.getElementById('result').innerHTML = result;
	document.getElementById('result1').innerHTML = '';
});


document.getElementById('readTable').addEventListener('submit', async function (e) {
	e.preventDefault();
	
	const formData = new FormData();
	formData.append('type_table',document.getElementById('type_table_1').value);
	
	if (document.getElementById('type_table_1').value == '_order') {
		formData.append('name_clientPlus', document.getElementById('name_clientPlus').value);
		formData.append('phone_clientPlus', document.getElementById('phone_clientPlus').value);
		formData.append('name_worderPlus', document.getElementById('name_worderPlus').value);
		formData.append('phone_worderPlus', document.getElementById('phone_worderPlus').value);
		formData.append('datePlus',document.getElementById('datePlus').value);
		formData.append('failurePlus',document.getElementById('failurePlus').value);
		formData.append('id_objectPlus',document.getElementById('id_objectPlus').value);
		formData.append('pricePlus',document.getElementById('pricePlus').value);
		formData.append('clients_wordsPlus',document.getElementById('clients_wordsPlus').value);
		formData.append('statusPlus',document.getElementById('statusPlus').value);
		formData.append('id_orderPlus',document.getElementById('id_orderPlus').value);
	}
	if (document.getElementById('type_table_1').value == 'object') {
		formData.append('namePlus',document.getElementById('namePlus').value);
	}
	if (document.getElementById('type_table_1').value == 'client') {
		formData.append('name_clientPlus', document.getElementById('name_clientPlus').value);
		formData.append('phone_clientPlus', document.getElementById('phone_clientPlus').value);
	}
	if (document.getElementById('type_table_1').value == 'worker') {
		formData.append('name_worderPlus', document.getElementById('name_worderPlus').value);
		formData.append('phone_worderPlus', document.getElementById('phone_worderPlus').value);
	}
	if (document.getElementById('type_table_1').value == 'human') {
		formData.append('namePlus',document.getElementById('namePlus').value);
		formData.append('phone_numberPlus',document.getElementById('phone_numberPlus').value);
	}
	
	const response = await fetch('serverReadTable.php', {
		method: 'POST',
		body: formData,
	});
	
	const result = await response.text();
	document.getElementById('result1').innerHTML = result;
	
	const formData1 = new FormData();
	formData1.append('type_table',document.getElementById('type_table_1').value);
	const response1 = await fetch('serverWriteTable.php', {
		method: 'POST',
		body: formData1,
	});
	
	const result1 = await response1.text();
	document.getElementById('result').innerHTML = result1;
	tableSpecialChek();
});