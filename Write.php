<?php
//データ受け取り
$name = $_POST['name'];
$title = $_POST['title'];
$body = $_POST['body'];
$pass = $_POST['pass'];

//必須項目チェック（名前欄が空出ないかの確認）
if ($name == '' || $body == ''){
  header('Location: keijiban.php');
  exit();//終了
}

//データベースに接続
$dsn = 'mysql:host=localhost;dbname=syouchan12_semi;charset=utf8';
$user = 'syouchan12_host';
$password = 'password'; //semiplayerに設定したパスワード

try {
  $db = new PDO($dsn, $user, $password);
  $db->setAttribute(PDO::ATTR_EMULATE_PREPARES,false);
  //プリペアドステートメントを作成
  $stmt = $db->prepare("
  INSERT INTO keijiban (name, title, body, date, pass)
  VALUE (:name, :title, :body, now(), :pass)"
);
//パラメータを割り当て
$stmt->bindParam(':name', $name, PDO::PARAM_STR);
$stmt->bindParam(':title', $title, PDO::PARAM_STR);
$stmt->bindParam(':body', $body, PDO::PARAM_STR);
$stmt->bindParam(':pass', $pass, PDO::PARAM_STR);
//クエリの実行
$stmt->execute();

//keijiban.phpに戻る
header('Location: keijiban.php');
exit();
}catch(PDOException $e) {
  die ('エラー：' . $e->getMessage());
}
 ?>
