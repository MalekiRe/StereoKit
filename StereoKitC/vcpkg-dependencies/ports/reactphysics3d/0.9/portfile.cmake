vcpkg_from_github(
  OUT_SOURCE_PATH SOURCE_PATH
  REPO DanielChappuis/reactphysics3d
  REF v0.9.0
  SHA512 e6216be99127f01065f6ada0df09e411a3b3196bbf6372f9c4912a526815f6e5784aacf7c127a40dccc839d801ec2c02fb88ee5de4d13da3f9d64a1f79769f2e
  HEAD_REF master
)

# SHA can be found by calling something like this:
# "C:\Repositories\vcpkg\vcpkg.exe" install reactphysics3d --overlay-ports=StereoKitC/vcpkg-dependencies/ports/reactphysics3d/0.9

vcpkg_cmake_configure(
  SOURCE_PATH "${SOURCE_PATH}"
)
vcpkg_cmake_install()

vcpkg_fixup_pkgconfig()
vcpkg_copy_pdbs()
file(INSTALL "${SOURCE_PATH}/LICENSE" DESTINATION "${CURRENT_PACKAGES_DIR}/share/${PORT}" RENAME copyright)