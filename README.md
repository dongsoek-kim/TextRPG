# TextRPG
 유니티 2주차 개인과자 
 Text RPG제작
 17조 김동석

MVC모델링을 참고한 프로그램입니다.
Viewer = Draw.cs
Controll = Gamemanager.cs
Model = 나머지
 ---------
ver 1.0.0

구현기능
1.게임시작화면
게임시작시 이름 직업을 받아 상태창 업로드

2.상태창
플레이어의 레벨,골드,능력치 표현

3.인벤토리

3-1.보유중인아이템
리스트를 통해 보유중인 아이템 관리
3-2.장착관리
각각의 부위별 착용여부를 비교하여 부위당 하나의 아이템만 장착가능
3-3.장착에 따른 캐릭터 스텟변화

4.상점
4-1.상점재구성
레벨업마다 레벨에따른 상점등장
4-1-1.상점이 재구성될때 이미 구매한아이템이면 출력안함
4-2.아이템구매
아이템구매시 인벤토리에 들어감
4-3.아이템판매
아이템 판매시 인벤토리에서 삭제됨,착용중이라면 착용해제

5.던전입장
난이도별로 던전입장,필요방어력과 획득재회 상이(밸런싱은 하지않음)

6.휴식하기
휴식시 체력회복

----------------
ver 1.1(예정)

저장하기.캐릭터 사망 구현
 
