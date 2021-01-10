import argparse, os
import zipfile
from os.path import basename
 
def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-p', '--project', type=str, required=True, help="Project name to build")
    parser.add_argument('-b', '--build', type=int, required=True, help="Build number to append to versioning")
    parser.add_argument('-o', '--output', type=str, default="..", help="Output folder")
    args = parser.parse_args()

    pack(args.project, [args.project], args.build, args.output)

def pack(project_name, projects, build_number, externals=[], output_prefix=".."):
    version = getVersion(project_name, build_number)
    output = "{}/bin/package/{}".format(output_prefix, project_name)
    print("Packing '{}' '{}' to output '{}'".format(project_name, version, output))

    zipfilename = "{}/{}-v{}.zip".format(output, project_name, version)

    os.makedirs(output, exist_ok=True)
    with zipfile.ZipFile(zipfilename, 'w') as zipObj:
        for project in projects:
            print("Adding project '{}' to zip".format(project))
            path = "{}/bin/builds/{}".format(output_prefix, project)
            addFileToZip(zipObj, "{}.deps.json".format(project), "Debug", path, project_name)
            addFileToZip(zipObj, "{}.dll".format(project)      , "Debug", path, project_name)
            addFileToZip(zipObj, "{}.pdb".format(project)      , "Debug", path, project_name)
            addFileToZip(zipObj, "{}.deps.json".format(project), "Release", path, project_name)
            addFileToZip(zipObj, "{}.dll".format(project)      , "Release", path, project_name)

        for external in externals:
            print("Adding external '{}' to zip".format(external))
            externalDir = "{}/{}/debug".format(output_prefix, external)
            for filename in os.listdir(externalDir):
                addFileToZip(zipObj, filename, "", externalDir, "{}/Debug/external/{}".format(project_name, external))
                
            externalDir = "{}/{}/release".format(output_prefix, external)
            for filename in os.listdir(externalDir):
                addFileToZip(zipObj, filename, "", externalDir, "{}/Release/external/{}".format(project_name, external))
        
        #Add readme and releasenotes
        addFileToZip(zipObj, "Readme-TypeO.txt", "", output_prefix, "{}/Debug".format(project_name))
        addFileToZip(zipObj, "Readme-TypeO.txt", "", output_prefix, "{}/Release".format(project_name))
        if os.path.isfile("{}/{}/ReleaseNotes-{}.txt".format(output_prefix, project_name, project_name)):
            addFileToZip(zipObj, "ReleaseNotes-{}.txt".format(project_name), project_name, output_prefix, "")
    return zipfilename
    
def addFileToZip(zipObj, filename, directory, pathFrom, pathTo):
    print("... Adding file: {}/{}/{}".format(pathFrom, directory, filename))
    zipObj.write("{}/{}/{}".format(pathFrom, directory, filename), "./{}/{}/{}".format(pathTo, directory, filename), zipfile.ZIP_DEFLATED)

def getVersion(project, build_number):
    path = "../{0}/version".format(project)

    with open(path) as f:
        return "{}.{}".format(f.readline().replace("\n", "").replace("\r", "").replace("v", ""), build_number)

if __name__ == "__main__":
    main()