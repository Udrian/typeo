cmake_minimum_required (VERSION 3.15)

set(PROJECT_NAME    "TypeOCore")
set(PROJECT_VERSION "0.1.0.0")
set(DOTNET_VERSION  "v4.7.2")
set(LANG_VERSION    "/langversion:7.3")
set(PROJECT_REFERENCES
	"System"
)
file(GLOB PROJECT_SRC
	"Typedeaf/TypeOCore/Game.cs"
	"Typedeaf/TypeOCore/TypeO.cs"
	"Typedeaf/TypeOCore/Module.cs"
	"Typedeaf/TypeOCore/Service.cs"
	"Typedeaf/TypeOCore/Scene.cs"
	"Typedeaf/TypeOCore/Graphics/Window.cs"
	"Typedeaf/TypeOCore/Graphics/Canvas.cs"
	"Typedeaf/TypeOCore/Content/ContentLoader.cs"
	"Typedeaf/TypeOCore/Content/Font.cs"
	"Typedeaf/TypeOCore/Content/Texture.cs"
	"Typedeaf/TypeOCore/Input/InputHandler.cs"
	"Typedeaf/TypeOCore/Input/KeyboardInput.cs"
	"Typedeaf/TypeOCore/Input/KeyConverter.cs"
	"Typedeaf/TypeOCore/Entities/Entity.cs"
	"Typedeaf/TypeOCore/Entities/Entity2D.cs"
)


project(${PROJECT_NAME} VERSION ${PROJECT_VERSION} LANGUAGES CSharp)

set(CMAKE_CSharp_FLAGS ${LANG_VERSION})

add_library(${PROJECT_NAME} SHARED ${PROJECT_SRC})

set_property( TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION ${DOTNET_VERSION} )
set_property( TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_REFERENCES ${PROJECT_REFERENCES} )

add_dependencies(${PROJECT_NAME} "TypeOCommon")