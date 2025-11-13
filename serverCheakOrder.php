<?php
function printExit($d = '')
{
	print $d; exit;
}

$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData)
	printExit("<p>Произошла ошибка соединения: ".pg_last_error());

$query = 'SELECT _o.id_order FROM _order _o WHERE _o.id_worker = '.$_POST['id'].' ORDER BY _o.id ';

$result = pg_query($basisData, $query);
if (!$result)
	printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

$object = [[]]; $i = 0;
$p = pg_num_fields($result);
while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{
	foreach ($line as $col_value)
	{
		if ($i == 0)
		{
			
		}
		else
		{

		}
	}
	$i++;
}

header('Content-type: application/json');
$data['object'] = $object;
echo json_encode($data);

pg_close($con);
?>
