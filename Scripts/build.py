import argparse, os
 
def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-p', '--project',      type=str, required=True,     help="Project name to build")
    parser.add_argument('-b', '--build_number', type=int, required=True,     help="Build number to append to versioning")
    parser.add_argument('-c', '--config',       type=str, default="Release", help="Debug|Release defaults to Release")
    parser.add_argument('-o', '--output',       type=str, default="..",      help="Output folder")
    args = parser.parse_args()

    build(args.project, args.build, args.config, args.output)

def build(project, build_number, config="Debug", output=".."):
    version = getVersion(project, build_number)
    output = "{}/bin/builds/{}/{}".format(output, project, config)
    print("Building '{}' '{}' '{}' to output '{}'".format(project, config, version, output))

    path = "../{}/{}.csproj".format(project, os.path.basename(project))

    #os.system("dotnet clean {} --configuration {} --output {} --verbosity n".format(path, config, output))
    os.system("dotnet publish {} --configuration {} --output {} --verbosity n /property:Version={}".format(path, config, output, version))

def getVersion(project, build_number):
    path = "../{0}/version".format(project)

    with open(path) as f:
        return "{}.{}".format(f.readline().replace("\n", "").replace("\r", "").replace("v", ""), build_number)

if __name__ == "__main__":
    main()