cmake_minimum_required (VERSION 3.15)
project("TypeO" CSharp)

# Add all the sub projects
add_subdirectory("SDL2")
add_subdirectory("TypeOCore")
add_subdirectory("TypeOCommon")
add_subdirectory("TypeOSDL")
add_subdirectory("Box2D")
add_subdirectory("SampleGame")

# Set the startup target project
set_property(DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR} PROPERTY VS_STARTUP_PROJECT "SampleGame")