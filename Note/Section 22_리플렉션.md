## 메타정보 가져오기
- 간단메모 :
    - Path.Combine(Environment.CurrentDirectory, "파일명")
    - Assembly.LoadFille("파일경로");
    - FileVersionInfo 클래스
    - reflection을 통해서 meta 정보를 쉽게 불러올 수 있다.

## 속성 정보 불러오기
- 간단메모 :
    - Type 클래스
        - Type.Name
        - Type.GetProperties() : public 속성만 보여준다.
        - Type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public) : Public, Private 전부 보여준다.

## 메서드 정보 불러오기
- 간단메모 :
    - Type 클래스
        - Type.GetMethods() : public 함수만 보여준다.
    
## 인스턴스 생성
- 간단메모 :
    - Activator 클래스
        - Activator.CreateInstance(Type) : 인자로 넣어준 타입에 해당하는 인스턴스 생성해서 반환한다.
        - Activator.CreateInstance<T>() : 위와 동일
## 동적 속성 값 읽기
- 간단메모 :
    - Type.GetProperty("속성명") : 인자로 넣어준 이름에 해당하는 속성에 접근하여 PropertyInfo 타입의 값 반환
    - PropertyInfo.GetValue(instance) : 인자로 넣어준 인스턴스가 가지고 있는 속성의 값을 반환, object? 타입의 값 반환
    - foreach (int i in Enumerable.Range(1,2))

## 동적 속성 값 쓰기
- 간단메모 :
    - PropertyInfo.SetValue(instance, value)

## 동적 필드 읽고 쓰기
- 간단메모 :
    - Type.GetField("필드명") : 인자로 넣어준 이름에 해당하는 필드에 접근하여 FieldInfo 타입의 값을 반환
    - FieldInfo.GetValue(instance)
    - FieldInfo.SetValue(instance, value) : private 필드의 값도 변경할 수 있다.
    - Type.GetField("필드명", BindingFlags.Instance | BindingFlags.NonPublic) : private 필드에 접근

## 동적 메서드 호출
- 간단메모 :
    - Type.GetMethod("함수명") : 인자로 넣어준 이름에 해당하는 함수에 접근하여 MethodInfo 타입의 값을 반환
    - MethodInfo.Invoke(instnace, null) : 함수 호출, 두번째 인자가 null인 이유는 요구하는 파라미터가 없기 때문

## 동적 속성 읽고 쓸때 주의사항
- 간단메모 :
    - PropertyInfo.CanRead : Getter 존재여부 확인 (true/false)
    - PropertyInfo.CanWrite : Setter 존재여부 확인 (true/false)
