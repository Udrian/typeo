import argparse, os
import zipfile
from os.path import basename
 
def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-p', '--project', type=str, required=True, help="Project name to build")
    parser.add_argument('-b', '--build', type=int, required=True, help="Build number to append to versioning")
    parser.add_argument('-o', '--output', type=str, default="..", help="Output folder")
    args = parser.parse_args()

    pack(args.project, args.build, args.output)

def pack(project, build_number, output_prefix=".."):
    version = getVersion(project, build_number)
    output = "{}/bin/package/{}".format(output_prefix, project)
    print("Packing '{}' '{}' to output '{}'".format(project, version, output))

    path = "{}/bin/builds/{}".format(output_prefix, project)
    
    def addFileToZip(zipObj, filename, directory, pathFrom, pathTo):
        zipObj.write("{}/{}/{}".format(pathFrom, directory, filename), "./{}/{}/{}".format(pathTo, directory, filename), zipfile.ZIP_DEFLATED)
    
    os.makedirs(output, exist_ok=True)
    with zipfile.ZipFile("{}/{}-v{}.zip".format(output, project, version), 'w') as zipObj:
        addFileToZip(zipObj, "{}.deps.json".format(project), "Debug", path, project)
        addFileToZip(zipObj, "{}.dll".format(project)      , "Debug", path, project)
        addFileToZip(zipObj, "{}.pdb".format(project)      , "Debug", path, project)
        addFileToZip(zipObj, "{}.deps.json".format(project), "Release", path, project)
        addFileToZip(zipObj, "{}.dll".format(project)      , "Release", path, project)
    
def getVersion(project, build_number):
    path = "../{0}/version".format(project)

    with open(path) as f:
        return "{}.{}".format(f.readline().replace("\n", "").replace("\r", "").replace("v", ""), build_number)

if __name__ == "__main__":
    main()