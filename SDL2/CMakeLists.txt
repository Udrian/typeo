cmake_minimum_required (VERSION 3.15)

#Define function for downloading a zip, extracting it and then deleting the zip
function(download_and_extract_file url filename)
if(NOT EXISTS "${CMAKE_CURRENT_BINARY_DIR}/${filename}")
	message("Downloading: ${filename}")
	
	#download
	file(DOWNLOAD ${url} "${CMAKE_CURRENT_BINARY_DIR}/${filename}" SHOW_PROGRESS)
	#extract
	message("Extracting files to folder: ${CMAKE_CURRENT_BINARY_DIR}")
	execute_process(COMMAND ${CMAKE_COMMAND} -E tar -xf ${filename} WORKING_DIRECTORY "${CMAKE_CURRENT_BINARY_DIR}")
endif()
endfunction(download_and_extract_file)

# Download SDL
download_and_extract_file(
  https://libsdl.org/release/SDL2-2.0.10-win32-x64.zip
  SDL2-2.0.10-win32-x64.zip
)
download_and_extract_file(
  https://www.libsdl.org/projects/SDL_image/release/SDL2_image-2.0.5-win32-x64.zip
  SDL2_image-2.0.5-win32-x64.zip
)
download_and_extract_file(
  https://www.libsdl.org/projects/SDL_ttf/release/SDL2_ttf-2.0.15-win32-x64.zip
  SDL2_ttf-2.0.15-win32-x64.zip
)