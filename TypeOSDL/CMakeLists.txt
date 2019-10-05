cmake_minimum_required (VERSION 3.15)

set(PROJECT_NAME    "TypeOSDL")
set(PROJECT_VERSION "0.1.0.0")
set(DOTNET_VERSION  "v4.7.2")
set(LANG_VERSION    "/langversion:7.3")
set(PROJECT_REFERENCES
	"System"
)
file(GLOB PROJECT_SRC
	"Typedeaf/TypeOSDL/SDLGame.cs"
	"Typedeaf/TypeOSDL/TypeOSDLModule.cs"
	"Typedeaf/TypeOSDL/Graphics/SDLWindow.cs"
	"Typedeaf/TypeOSDL/Graphics/SDLCanvas.cs"
	"Typedeaf/TypeOSDL/Content/SDLContentLoader.cs"
	"Typedeaf/TypeOSDL/Content/SDLFont.cs"
	"Typedeaf/TypeOSDL/Content/SDLTexture.cs"
	"Typedeaf/TypeOSDL/Input/SDLKeyboardInput.cs"
)


project(${PROJECT_NAME} VERSION ${PROJECT_VERSION} LANGUAGES CSharp)

set(CMAKE_CSharp_FLAGS ${LANG_VERSION})

add_library(${PROJECT_NAME} SHARED ${PROJECT_SRC})

set_property( TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION ${DOTNET_VERSION} )
set_property( TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_REFERENCES ${PROJECT_REFERENCES} )

add_dependencies(${PROJECT_NAME} "TypeOCommon")
add_dependencies(${PROJECT_NAME} "TypeOCore")

set_property(TARGET ${PROJECT_NAME} PROPERTY 
    VS_DOTNET_REFERENCE_SDL2-CS "${CMAKE_CURRENT_SOURCE_DIR}/../TypeOSDL/SDL2-CS.dll")