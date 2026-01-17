## Attribute 정의 및 적용하기
- 간단메모 :
    - Attribute는 클래스, 메서드, 속성 등에 추가적인 정보를 제공하는 메타데이터
    - 이를 통해 컴파일러나 런타임에 특정 동작을 수행하거나, 코드에 대한 추가적인 정보를 활용할 수 있다.
    - 주요 특징 : 메타데이터 제공, 컴파일러 지시, 런타임 정보, 재사용 가능
    - \[Obsolete\] : 더이상 사용되지 않음을 나타낸다. ObsoleteAttribute로 작성해도 되지만 Attribute는 생략해도 된다.
    - Attribute 는 일반적으로 클래스로 만들어져있으며 Attribute 클래스를 상속받고 있다.
    - \[Conditional("솔루션구성")\] : 호출 환경을 설정할 수 있다. (RELEASE, DEBUG)

## 매개변수에서 사용되는 Attribute
- 간단메모 :
    - \[CallerMemberName\] : "System.Runtime.CompilerServices", 해당 속성이 적용되는 매개변수는 선택적 매개변수로 만들어줘야한다. 메서드가 호출된 위치의 메서드 이름을 호출한다.
    - \[CallerFilePath\] : 해당 속성이 적용되는 매개변수는 선택적 매개변수로 만들어줘야한다. 해당 메서드가 어느 파일에서 호출되었는지 알려준다.
    - \[CallerLineNumber\] : 해당 속성이 적용되는 매개변수는 선택적 매개변수로 만들어줘야한다. 해당 메서드가 호출된 라인의 넘버를 알려준다.
    
## CustomAttribute 생성
- 간단메모 :
    - 커스텀 Attribute 클래스를 만들기위해선 Attribute 클래스를 상속받아야한다.
    - 클래스의 이름은 관례로 접미사로 "Attribute" 를 붙여줘야한다. (예시 MyCustomAttribute)

## CustomAttribute 생성자 매개변수, 속성 추가
- 간단메모 :
    - 커스텀 Attribute의 생성자 생성시 파라미터를 추가할 수 있다.
    - Attribute 적용시 생성자에 파라미터가 요구된다면 추가해줘야하며
    - \[MyCustom(name : "custom", Description = "내용")\] 과 같이 Attribute 클래스의 프로퍼티의 값도 지정해줄 수 있다.

## CustomAttribute 사용 범위 제어하기
- 간단메모 :
    - \[AttributeUsage(AttributeTargets)\] : Attribute 의 사용범위를 지정할 수 있다. 기본적으로 해당 설정을 하지 않으면 어디든 사용할 수 있게된다.
    - AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property

## CustomAttribute 동작 제어
- 간단메모 :
    - AllowMultiple = true : 기본값 false, 동일한 Attribute 를 여러번 사용할 수 있도록 해준다.
    - Inherited = true : 기본값 true, 크래스 상속시 Attribute도 같이 상속할지 설정한다.

## CustomAttribute 활용 예제 (메타 정보 읽어오기)
- 간단메모 :
    - 
## CustomAttribute 실전 활용 예제 (Property)
- 간단메모 :
    - 
## AOP (Aspect-Oriented Programming) 구현
- 간단메모 :
    - 
## CustomAttribute 실전 활용 예제 (Parameter)
- 간단메모 :
    - 