<?php
//1ページに表示されるコメントの数
$num = 10;

//データベースに接続
$dsn = 'mysql:host=localhost;dbname=syouchan12_semi;charset=utf8';
$user = 'semiplayer';
$password = 'password'; //semiplayerに設定したパスワード

//ページ数が設定されている時
$page = 0;
if (isset($_GET['page']) && $_GET['page'] > 0){
  $page = intval($_GET['page']) -1;
}

try {
  $db = new PDO($dsn, $user, $password);
  $db->setAttribute(PDO::ATTR_EMULATE_PREPARES, false);
  //プリペアドステートメント
  $stmt = $db->prepare(
    "SELECT * FROM keijiban ORDER BY date DESC LIMIT :page, :num"
  );

  //パラメータ割り当て
  $page = $page * $num;
  $stmt->bindParam(':page', $page, PDO::PARAM_INT);
  $stmt->bindParam(':num', $num, PDO::PARAM_INT);
  //クエリの実行
  $stmt->execute();
} catch(PDOException $e){
  echo "エラー：" . $e->getMessage();
}
 ?>
<!DOCTYPE html>
<html lang="ja" dir="ltr">
  <head>
    <meta charset="utf-8">
    <title>ゼミ掲示板</title>
  </head>
  <body>
    <h1>ゼミ掲示板</h1>
    <a href="index.php"target="_blank">戻る</a>
    <p><a href="index.html"></a></p>
    <form action="Write.php" method="post">
      <p>ネーム:<input type="text" name="name" placeholder="名前"required></p>
      <p>タイトル:<input type="text" name="title" placeholder="タイトル"required></p>
      <textarea name="body" rows="8" cols="80"required></textarea>
      <p>キーコード：<input type="text" name="pass" placeholder="キーコード"></p>
      <p><input type="submit" value="書き込み"onclick="alert('送信しました。');"></p>
    </form>
    <hr />
    <hr />
    <?php
    while ($row = $stmt->fetch()):
      $title = $row['title'] ? $row['title'] : '(無題)';
     ?>
     <p>名前：<?php echo $row['name'] ?></p>
     <p>タイトル：<?php echo $title ?></p>
     <p><?php echo nl2br($row['body'], false) ?></p>
     <p><?php echo $row['date'] ?></p>
     <?php
   endwhile;

   //ページ数の表示
   try {
     // プリペアドステートメント作成
     $stmt = $db->prepare("SELECT COUNT(*) FROM keijiban");
     //クエリの実行
     $stmt->execute();
   } catch (PDOExeption $e){
     echo "エラー：" . $e->getMessage();
   }

   //コメント件数を取得
   $comments = $stmt->fetchColumn();
   //ページ数を計算
   $max_page = ceil($comments / $num);
   echo '<p>';
   for ($i = 1; $i <= $max_page; $i++){
     echo'<a href="keijiban.php?page=' . $i . '">' . $i .'</a>&nbsp;';
   }
   echo '</p>';
      ?>
  </body>
</html>
