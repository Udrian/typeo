cmake_minimum_required (VERSION 3.15)

set(PROJECT_NAME    "SampleGameSDLWin")
set(PROJECT_VERSION "0.1.0.0")
set(DOTNET_VERSION  "v4.7.2")
set(LANG_VERSION    "/langversion:7.3")
set(PROJECT_REFERENCES
	"System"
)
file(GLOB PROJECT_SRC
	"Program.cs"
)


project(${PROJECT_NAME} VERSION ${PROJECT_VERSION} LANGUAGES CSharp)

set(CMAKE_CSharp_FLAGS ${LANG_VERSION})

add_executable(${PROJECT_NAME} ${PROJECT_SRC})

set_property( TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION ${DOTNET_VERSION} )
set_property( TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_REFERENCES ${PROJECT_REFERENCES} )

add_dependencies(${PROJECT_NAME} "TypeOCommon")
add_dependencies(${PROJECT_NAME} "TypeOCore")
add_dependencies(${PROJECT_NAME} "TypeO2D")
add_dependencies(${PROJECT_NAME} "TypeOSDL")
add_dependencies(${PROJECT_NAME} "SampleGameCore")
