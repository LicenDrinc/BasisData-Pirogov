<?php
function readTable()
{
	global $line;
	foreach ($line as $col_value)
		print "<td class='boxC1'>".$col_value."</td>";
}

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
	$data = $_POST['idOrder'];
	$table = $_POST['table'];
	
	if ($table == '_order')
	{
		//$id_client = $_POST['id_client'];
		//$id_worker = $_POST['id_worker'];
		$date = $_POST['date'];
		$name_client = $_POST['name_client'];
		$name_worker = $_POST['name_worder'];
		$phone_number_client = $_POST['phone_client'];
		$phone_number_worker = $_POST['phone_worder'];
		$id_object = $_POST['id_object'];
		$id_order = $_POST['id_order'];
		//$id_object = $_POST['id_object'];
	}
	else if ($table == 'object')
	{
		$id_object = $_POST['id_object'];
	}
	else if ($table == 'client')
	{
		//$id_human = $_POST['id_human'];
		$name_client = $_POST['name_client'];
		$phone_number_client = $_POST['phone_client'];
	}
	else if ($table == 'worker')
	{
		//$id_human = $_POST['id_human'];
		$name_worker = $_POST['name_worder'];
		$phone_number_worker = $_POST['phone_worder'];
	}
	else if ($table == 'human')
	{
		$phone_number = $_POST['phone_number'];
		$name = $_POST['name'];
	}
}
else
{
	print "нет POST"; exit;
}

$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData) {
	print "<p>Произошла ошибка соединения: ";
	print pg_last_error(); exit;
}

if ($table == "_order")
	$query = 'SELECT '.
		'h1.name AS "Имя клиента", h1.phone_number AS "Номер телефон клиента", '.
		'h2.name AS "Имя работника", h2.phone_number AS "Номер телефон работника", '.
		'_o.date AS "Дата", _o.failure AS "Поломка", o.name AS "Название объекта", '.
		'_o.price AS "Цена", _o.clients_words AS "Со слов клиента", _o.status AS "Статус", _o.id_order AS "Заказ" '.
		'FROM _order _o '.
		'INNER JOIN client c ON _o.id_client=c.id '.
		'INNER JOIN human h1 ON c.id_human=h1.id '.
		'INNER JOIN worker w ON _o.id_worker=w.id '.
		'INNER JOIN human h2 ON w.id_human=h2.id '.
		'INNER JOIN object o ON _o.id_object=o.id '.
		'GROUP BY _o.id, "Название объекта", '.
		'"Номер телефон клиента", "Имя работника", '.
		'"Номер телефон работника", "Имя клиента", c.id_human, w.id_human '.
		($date != null || $name_client != '' || $phone_number_client != '' || $name_worker != '' || $phone_number_worker != '' || $id_object != '' || $id_order != null  ?
			('HAVING '.
			($date != null ? '_o.date = \''.$date.'\' AND ' : '').
			($name_client != '' ? 'c.id_human = '.$name_client.' AND ' : '').
			($phone_number_client != '' ? 'c.id_human = '.$phone_number_client.' AND ' : '').
			($name_worker != '' ? 'w.id_human = '.$name_worker.' AND ' : '').
			($phone_number_worker != '' ? 'w.id_human = '.$phone_number_worker.' AND ' : '').
			($id_object != '' ? '_o.id_object = '.$id_object.' AND ' : '').
			($id_order != null ? '_o.id_order = '.$id_order.' ' : '')
			) : ''
		);
else if ($table == "client" || $table == "worker")
	$query = 'SELECT '.
		($table == "client" ? 
			'h.name AS "Имя клиента", h.phone_number AS "Номер телефон клиента"' : 
			'h.name AS "Имя работника", h.phone_number AS "Номер телефон работника"'
		).
		' FROM '.$table.' c '.
		($table == "client" ? 
			'INNER JOIN human h ON c.id_human=h.id' :
			'INNER JOIN human h ON c.id_human=h.id'
		).
		' GROUP BY c.id_human, '.
		($table == "client" ? 
			'"Номер телефон клиента", "Имя клиента"' : 
			'"Номер телефон работника", "Имя работника"'
		).
		(($table == "client" ? ($name_client != '' || $phone_number_client != '') : ($name_worker != '' || $phone_number_worker != '')) ?
			(' HAVING '.
			($table == "client" ?
				(($name_client != '' ? 'c.id_human = '.$name_client.' AND ' : '').
				($phone_number_client != '' ? 'c.id_human = '.$phone_number_client.' ' : '')) : 
				(($name_worker != '' ? 'c.id_human = '.$name_worker.' AND ' : '').
				($phone_number_worker != '' ? 'c.id_human = '.$phone_number_worker.' ' : '')))
			) : ''
		);
else
	$query = 'SELECT '.
	($table == "object" ? 'name AS "Название объекта"' : (
	$table == "human" ? 'name AS "Имя", phone_number AS "Телефон клиента"' : '')).
	' FROM '.$table.
	' GROUP BY id '.
	(
		($table == "object" ? ($id_object != '') : ($table == "human" ? ($phone_number != '' || $name != '') : false))
		?
		(' HAVING '.
		(
			$table == "object"
			?
				($id_object != '' ? 'id = '.$id_object.' ' : '') 
			: 
				(
					$table == "human"
					? 
					(($name != '' ? 'id = '.$name.' AND ' : '').
					($phone_number != '' ? 'id = '.$phone_number.' ' : ''))
					:
					''
				)
		)
		) : ''
	);

if ($query[strlen($query)-4] == 'A')
		$query = substr($query, 0, strlen($query)-4);

$result = pg_query($basisData, $query);
if (!$result) {
	print "<p>Ошибка запроса: <p>";
	print pg_last_error();
	print "<p>".$query;
	exit;
}

print "<table border='1' class='boxA1' rules='all'><tr class='boxB1'>";
$p = pg_num_fields($result);
for ($i = 0; $i < $p; $i++)
	print "<th>".pg_field_name($result, $i)."</th>";
print "</tr>";

while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{
	print "<tr class='boxB2'>";
	/*
	if ($table == '_order')
	{
		if (($date == null || $line['Дата'] == $date) && 
			($name_client == null || $line['Имя клиента'] == $name_client) &&
			($phone_number_client == null || $line['Номер телефон клиента'] == $phone_number_client) &&
			($name_worker == null || $line['Имя работника'] == $name_worker) &&
			($phone_number_worker == null || $line['Номер телефон работника'] == $phone_number_worker))
			readTable();
	}
	else if ($table == 'object')
	{
		if(($data == null || $line['id'] == $data))
			readTable();
	}
	else if ($table == 'client')
	{
		if (($id_human == null || $line['id_human'] == $id_human) &&
			($data == null || $line['id'] == $data))
			readTable();
	}
	else if ($table == 'worker')
	{
		if (($id_human == null || $line['id_human'] == $id_human) &&
			($data == null || $line['id'] == $data))
			readTable();
	}
	else if ($table == 'human')
	{
		if (($phone_number == null || $line['phone_number'] == $phone_number) &&
			($data == null || $line['id'] == $data))
			readTable();
	}
	*/
	readTable();
	print "</tr>";
}
print "</table>";
pg_close($con);
//print "<form action='indexClient.html'><p><button type='submit'>вернутся</button></p></form>";
?>
