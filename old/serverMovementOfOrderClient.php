<?php
function readTable()
{
	global $line;
	foreach ($line as $col_value)
		print "<td class='boxC1'>".$col_value."</td>";
}

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
	$data = $_POST['id_order'];
	if (!$data)
	{
		print "<p>Пустое значение "; exit;
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
	'HAVING _o.id_order='.$data.'';

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
	readTable();
	print "</tr>";
}
print "</table>";
pg_close($con);
//print "<form action='indexClient.html'><p><button type='submit'>вернутся</button></p></form>";
?>
