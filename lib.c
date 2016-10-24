#include <sys/types.h>
#include <sys/stat.h>
#include <string.h>
#include <errno.h>
#include <unistd.h>

int makeSock(const char* file, int mode)
{
  return mkfifo(file, mode);
}

int deleteSock(const char* file)
{
  return unlink(file);
}

char* getLastErrorStr(void)
{
  return strerror(errno);
}

int getLastErrorInt(void)
{
  return errno;
}
