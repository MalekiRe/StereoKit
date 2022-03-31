vcpkg_from_github(
  OUT_SOURCE_PATH SOURCE_PATH
  REPO KhronosGroup/OpenXR-SDK
  REF release-1.0.22
  SHA512 fe3c393c2d11981b42355acd8dbc337727120bcd0ff595abac1975c4ce5f68bb74a9a1b4c959e64e9a847ae5d504100d31979ffd7d9702c55b2dbd889de17d3e
  HEAD_REF master
)

# Weird behavior inside the OpenXR loader.  On Windows they force shared libraries to use static crt, and
# vice-versa. Might be better in future iterations to patch the CMakeLists.txt for OpenXR
set(DYNAMIC_LOADER OFF)
set(VCPKG_CRT_LINKAGE static)
set(VCPKG_BUILD_TYPE release)

vcpkg_find_acquire_program(PYTHON3)

vcpkg_cmake_configure(
    SOURCE_PATH "${SOURCE_PATH}"
    OPTIONS
        -DBUILD_API_LAYERS=OFF
        -DBUILD_TESTS=OFF
        -DBUILD_CONFORMANCE_TESTS=OFF
        -DDYNAMIC_LOADER=OFF
        -DPYTHON_EXECUTABLE="${PYTHON3}"
)

file(REMOVE_RECURSE "${CURRENT_PACKAGES_DIR}/debug/include")
file(REMOVE_RECURSE "${CURRENT_PACKAGES_DIR}/debug/share")

vcpkg_fixup_pkgconfig()
vcpkg_copy_pdbs()
file(INSTALL "${SOURCE_PATH}/LICENSE" DESTINATION "${CURRENT_PACKAGES_DIR}/share/${PORT}" RENAME copyright)