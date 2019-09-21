cmake_minimum_required (VERSION 3.15)

set(PROJECT_NAME    "TypeOCore")
set(PROJECT_VERSION "0.1.0.0")
set(DOTNET_VERSION  "v4.7.2")
set(LANG_VERSION    "/langversion:7.3")
set(PROJECT_REFERENCES
	"System"
)
file(GLOB PROJECT_SRC
	"Typedeaf/TypeO/Engine/Core/Game.cs"
	"Typedeaf/TypeO/Engine/Core/TypeO.cs"
	"Typedeaf/TypeO/Engine/Core/Module.cs"
	"Typedeaf/TypeO/Engine/Core/Service.cs"
	"Typedeaf/TypeO/Engine/Core/Scene.cs"
	"Typedeaf/TypeO/Engine/Graphics/Window.cs"
	"Typedeaf/TypeO/Engine/Graphics/Canvas.cs"
	"Typedeaf/TypeO/Engine/Content/Texture.cs"
	"Typedeaf/TypeO/Engine/Input/InputHandler.cs"
	"Typedeaf/TypeO/Engine/Input/KeyboardInput.cs"
	"Typedeaf/TypeO/Engine/Input/KeyConverter.cs"
)


project(${PROJECT_NAME} VERSION ${PROJECT_VERSION} LANGUAGES CSharp)

set(CMAKE_CSharp_FLAGS ${LANG_VERSION})

add_library(${PROJECT_NAME} SHARED ${PROJECT_SRC})

set_property( TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION ${DOTNET_VERSION} )
set_property( TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_REFERENCES ${PROJECT_REFERENCES} )

add_dependencies(${PROJECT_NAME} "TypeOCommon")