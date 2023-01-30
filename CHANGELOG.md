# Changelog
## [1.0.13] - 30/01/2023
### Changed
- Simplificação de operações logicas.
- Remoção de atribuições desnecessárias.
- Transformando possiveis campos em `readonly`.
## [1.0.9] 27/08/2022
### Rename
- Runtime\CobilasGC.cs > Runtime\GarbageCollector.cs
## [1.0.8] 13/08/2022
- Change Editor\Cobilas.Unity.Editor.Mono.asmdef
- Change Runtime\Cobilas.Unity.Mono.asmdef
## [1.0.7] 01/08/2022
- Fix CHANGELOG.md
- Fix package.json
### Change CobilasBehaviour.cs
#### Métodos adicionados
- > `public static GameObject FindObjectByName(string);`
- > `public static T FindObjectByName<T>(string) where T : UEObject;`
## [1.0.6] 01/08/2022
- Fix CHANGELOG.md
- Fix package.json
### Change CobilasBehaviour.cs
#### Métodos adicionados
- > `void:SetPosition(Vector3);`
- > `void:SetPosition(Vector2);`
- > `void:SetPosition(float, float);`
- > `void:SetPosition(float, float, float);`
- > `void:SetLocalPosition(Vector3);`
- > `void:SetLocalPosition(Vector2);`
- > `void:SetLocalPosition(float, float);`
- > `void:SetLocalPosition(float, float, float);`
- > `Vector3:GetPosition();`
- > `Vector3:GetLocalPosition();`
- >
- > `void:SetEulerAngles(Vector3);`
- > `void:SetEulerAngles(Vector2);`
- > `void:SetEulerAngles(float, float);`
- > `void:SetEulerAngles(float, float, float);`
- > `void:SetLocalEulerAngles(Vector3);`
- > `void:SetLocalEulerAngles(Vector2);`
- > `void:SetLocalEulerAngles(float, float);`
- > `void:SetLocalEulerAngles(float, float, float);`
- > `Vector3:GetEulerAngles();`
- > `Vector3:GetLocalEulerAngles();`
- >
- > `void:LocalScale(Vector3);`
- > `void:LocalScale(Vector2);`
- > `void:LocalScale(float, float);`
- > `void:LocalScale(float, float, float);`
- > `Vector3:GetLossyScale();`
- > `Vector3:GetLocalScale();`
- >
- > `void:SetRotation(Quaternion);`
- > `void:SetRotation(Vector4);`
- > `void:SetRotation(float, float, float, float);`
- > `void:SetLocalRotation(Quaternion);`
- > `void:SetLocalRotation(Vector4);`
- > `void:SetLocalRotation(float, float, float, float);`
- > `Quaternion:GetRotation();`
- > `Quaternion:GetLocalRotation();`
## [1.0.5] 31/07/2022
- Fix CHANGELOG.md
- Fix package.json
- Add Cobilas Mono.asset
- Remove Runtime\DependencyWarning.cs
- Remove Editor\DependencyWarning.cs
## [1.0.4] 23/07/2022
- Add CHANGELOG.md
- Fix package.json
## [1.0.3] 22/07/2022
- Fix LICENSE.md
- Fix package.json
- Add Cobilas.Unity.Editor.Mono.asmdef
- Add Cobilas.Unity.Mono.asmdef
- Add Editor/DependencyWarning.cs
- Add Runtime/DependencyWarning.cs
## [1.0.2] 17/07/2022
- Fix package.json
- Delete main.yml
- Delete README.md
## [1.0.0] 15/07/2022
- Add main.yml
- Add package.json
- Add LICENSE.md
- Add folder:Editor
- Add folder:Runtime
## [0.0.1] 15/07/2022
### Repositorio com.cobilas.unity.mono iniciado
- Lançado para o GitHub