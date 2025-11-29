<?php
function printExit($d = '')
{
	print $d; exit;
}

$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData)
	printExit("<p>Произошла ошибка соединения: ".pg_last_error());

$query = 'SELECT _o.id, h1.name AS "name1", h1.phone_number AS "pn1", '.
	'_o.date, _o.failure, o.name, _o.price, _o.clients_words, _o.status, _o.id_order FROM _order _o '.
	'INNER JOIN worker c ON _o.id_client=c.id INNER JOIN human h1 ON c.id_human=h1.id '.
	'INNER JOIN object o ON _o.id_object=o.id WHERE _o.id_order = '.$_POST['id'].' ORDER BY _o.id ';

$result = pg_query($basisData, $query);
if (!$result)
	printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

if ($_POST['mode'] == 0) $object = [];
if ($_POST['mode'] == 1) $object = [[]];
$p = pg_num_fields($result);
$scam = 0;
while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{
	$i = 0; $newData = 0;
	if ($_POST['mode'] == 0)
	{
		foreach ($line as $col_value)
		{
			if ($i == 0)
			{
				if ($scam < $col_value)
				{
					$newData = 1;
					$scam = $col_value;
				}
			}
			else
			{
				if ($newData == 1)
					$object[$i - 1] = $col_value;
			}
			$i++;
		}
	}
	if ($_POST['mode'] == 1)
	{
		foreach ($line as $col_value)
		{
			if ($i != 0)
				$object[$scam][$i - 1] = $col_value;
			$i++;
		}
		$scam++;
	}
}

header('Content-type: application/json');
$data['object'] = $object;
echo json_encode($data);

pg_close($basisData);
?>
