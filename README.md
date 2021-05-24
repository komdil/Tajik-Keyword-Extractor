# Tajik Keyword Extractor 
Модули барнома барои муайян кардани калимаҳои 
калидии матни ношинос дар забони тоҷикӣ дар забони барномарезии C#. 
Дар модули мазкур барои муайян кардани калимаҳои калидӣ аз алгоритми <a href="https://en.wikipedia.org/wiki/Keyword_extraction">KEA</a> ва 
метрикаи <a href="https://en.wikipedia.org/wiki/Tf%E2%80%93idf">TF-IDF</a> истифода бурда шудааст. 
_________________________


# Имкониятҳои модул:

Муайян кардани калимаҳои калидӣ дар матни ношинос

Муайян кардани калимаҳои калидӣ дар матни ношинос вобаста ба категория

Ҳисоб кардани TF

Ҳисоб кардани IDF

Ҳисоб кардани TF-IDF
_______________________________


# Базаи маълумоти калимаҳо

Лоиҳаи TajikKEA бо истифода аз интерфейсҳо сохта шудааст. Он имконият медиҳад, ки Шумо 
бо структураи муайяни базаи маълумот базаи маълумоти калимаҳои худро тартиб диҳед. 
Дар лоиҳаи TajikKEAJsonContext намунаи базаи маълумоти калимаҳо оварда шудааст. 
Дар он беш аз 50,000 калима ва бузургии муҳимияти он коркард карда шудааст. Инчунин баъзе аз 
категорияҳо низ оварда шудаанд. Базаи маълумот ба намуди JSON сабт шудааст. 
Намунаи муайян кардани бузургии муҳимияти калима аз рӯи кетегорияҳои гуногун дар 
лоиҳаи https://github.com/komdil/TajikKEAHelpers нишон дода шудааст.
_____________________________________________

# Истифодабарии модул
1. Насб кардани модул аз Nuget: https://www.nuget.org/packages/TajikKEA/

2. Насб кардани базаи маълумот. Масалан лоиҳаи <a href="https://www.nuget.org/packages/TajikKEAJsonContext/">TajikKEAJsonContext</a>

3. Пеш аз истифода бурдан бояд танзимоти базаи маълумот насб карда шавад:
```
TajikKEAContext jsonContext = new TajikKEAContext();
KEAGlobal.InitiateKEAGlobal(jsonContext);
```
4. Истифодабарӣ:
  
  Муайян кардани калимаҳои калидӣ дар матни ношинос
  ```
	var keyWords = KEAGlobal.KEAManager.GetKeywords(InputText, 20);
  ```
	
  Муайян кардани калимаҳои калидӣ дар матни ношинос вобаста ба категория
  

  ```
  var category = KEAGlobal.Context.Categories.Single(s => s.Name == "Сиёсӣ"); 
  var keyWords = KEAGlobal.KEAManager.GetKeywords(InputText, 20, category);
  ``` 
	
	дар ин ҷо category - категория, InputText - матн, 20- миқдори калид-калима, 
	keyWords- рӯйхати калид-калимаҳо


Ҳисоб кардани IDF 
	 ```KEAGlobal.TFIDFManager.CalCulateIDF() ```
	

Ҳисоб кардани TF
	```KEAGlobal.TFIDFManager.CalCulateTF()```
	

Ҳисоб кардани TF-IDF
	```KEAGlobal.TFIDFManager.CalculateTFIDF()```

________________________________


# Саҳми худро гузоред (<a href="https://github.blog/2013-01-07-introducing-contributions/#:~:text=You're%20making%20contributions%20to,sorted%20by%20your%20recent%20impact.">Contributing</a>)

Лоиҳаи мазкур барои ислоҳ кардан ва ҳамроҳ кардани имкониятҳо нав озод аст
1. Гирифтани лоиҳа (<a href="https://git-fork.com/">Fork</a>)
2. Сохтани шоха (<a href="https://git-scm.com/docs/git-branch">Branch</a>)
3. Илова намудани тағйиротҳо (<a href="https://github.com/git-guides/git-commit">Commit</a>)
4. Сабти тағйиротҳо (<a href="https://git-scm.com/docs/git-push">Push</a>)
5. Фиристонидан барои тафтиш (<a href="https://docs.github.com/en/github/collaborating-with-issues-and-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request">Pull request</a>)
______________________________________

# Литсензия
Тибқи литсензияи <a href="https://ru.wikipedia.org/wiki/%D0%9B%D0%B8%D1%86%D0%B5%D0%BD%D0%B7%D0%B8%D1%8F_MIT">MIT</a>.
Саҳмгузорон (<a href="https://github.com/komdil">komdil</a>, qosimovabdunabi)
_____________________________________________

